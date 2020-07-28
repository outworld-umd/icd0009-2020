using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _mapper = new OrderMapper();
        private readonly OrderRowMapper _rowMapper = new OrderRowMapper();
        private readonly OrderItemChoiceMapper _choiceMapper = new OrderItemChoiceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Order
        /// <summary>
        /// Get orders for single session 
        /// </summary>
        /// <returns>orders for session</returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Order>>> GetOrders()
        {
            var userTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            
            return Ok((await _bll.Orders.GetAllAsync(userTKey)).Select(e => _mapper.MapOrder(e)));
        }
        
        // GET: api/Item/restaurant/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("restaurant/{id}")]
        [Authorize(Roles = "Restaurant")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Item))]
        public async Task<ActionResult<IEnumerable<V1DTO.Item>>> GetOrdersByRestaurant(Guid id) {
            if (!await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(id))) 
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            return Ok((await _bll.Orders.GetAllByRestaurantAsync(id)).Select(e => _mapper.MapOrder(e)));
        }

        // GET: api/Order/5
        /// <summary>
        /// Get a single order
        /// </summary>
        /// <param name="id">id for order</param>
        /// <returns>order</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.Order))]
        public async Task<ActionResult<V1DTO.Order>> GetOrder(Guid id)
        {
            var userTKey = User.IsInRole("Customer") ? (Guid?) User.UserGuidId() : null;

            var order = await _bll.Orders.FirstOrDefaultAsync(id, userTKey);

            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Order with id {id} not found"));
            }

            if (User.IsInRole("Restaurant") && await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(order.RestaurantId))) 
            {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }

            return Ok(_mapper.Map(order));
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOrder(Guid id, V1DTO.Order order)
        {
            if (User.IsInRole("Restaurant") && await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(order.RestaurantId))) 
            {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }
            
            if (id != order.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and Order.Id do not match"));
            }

            await _bll.Orders.UpdateAsync(_mapper.Map(order));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new order
        /// </summary>
        /// <param name="order">Order info</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Order))]
        [HttpPost]
        public async Task<ActionResult<V1DTO.Order>> PostOrder(V1DTO.Order order)
        {
            if (User.IsInRole("Restaurant") && await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(order.RestaurantId))) 
            {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }
            var bllEntity = _mapper.Map(order);
            bllEntity.AppUserId = User.UserGuidId();
            _bll.Orders.Add(bllEntity);
            await _bll.SaveChangesAsync();
            order.Id = bllEntity.Id;

            return CreatedAtAction("GetOrder",
                new {id = order.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                order);
        }
        
        // PATCH: api/Order/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderPatch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PatchOrder(Guid id, V1DTO.OrderPatch orderPatch) 
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            
            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Order with id {id} not found"));
            }
            
            if (User.IsInRole("Restaurant") && await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(order.RestaurantId))) 
            {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }

            order.OrderStatus = orderPatch.OrderStatus;

            await _bll.Orders.UpdateAsync(order);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Order/5
        /// <summary>
        /// Deletes the order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            if (order == null)
            {
                return NotFound(new V1DTO.MessageDTO("Order not found"));
            }
            
            if (User.IsInRole("Restaurant") && await _bll.RestaurantUsers.AnyAsync(ru =>
                ru.AppUserId.Equals(User.UserGuidId()) && ru.RestaurantId.Equals(order.RestaurantId))) 
            {
                return Unauthorized(new V1DTO.MessageDTO("User not authorized for this restaurant"));
            }

            await _bll.Orders.RemoveAsync(order);
            await _bll.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(Guid id)
        {
            return _bll.Orders.Exists(id);
        }
    }
}
