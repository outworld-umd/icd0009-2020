using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved item choices of orders
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemChoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderItemChoiceMapper _mapper = new OrderItemChoiceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderItemChoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderItemChoice
        /// <summary>
        /// Get a all the item choices of orders
        /// </summary>
        /// <returns>Array consisting of item choices of orders</returns>
        /// 
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderItemChoice>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderItemChoice>>> GetOrderItemChoices()
        {
            return Ok((await _bll.OrderItemChoices.GetAllAsync()).Select(e => _mapper.MapOrderItemChoice(e)));
        }

        // GET: api/OrderItemChoice/5
        /// <summary>
        /// Get a single order item choice
        /// </summary>
        /// <param name="id">Id for order item choice</param>
        /// <returns>order item choice</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderItemChoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.OrderItemChoice>> GetOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id);

            if (orderItemChoice == null)
            {
                return NotFound(new V1DTO.MessageDTO($"OrderItemChoice with id {id} not found"));
            }

            return Ok(_mapper.Map(orderItemChoice));
        }

        // PUT: api/OrderItemChoice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update the order item choice
        /// </summary>
        /// <param name="id">Id for order item choice</param>
        /// <param name="orderItemChoice">Edited order item choice</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOrderItemChoice(Guid id, V1DTO.OrderItemChoice orderItemChoice)
        {
            if (id != orderItemChoice.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and OrderItemChoice.Id do not match"));
            }

            await _bll.OrderItemChoices.UpdateAsync(_mapper.Map(orderItemChoice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderItemChoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new order item choice
        /// </summary>
        /// <param name="orderItemChoice">New order item choice info</param>
        /// <returns>Newly created order item choice</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.OrderItemChoice))]
        public async Task<ActionResult<V1DTO.OrderItemChoice>> PostOrderItemChoice(V1DTO.OrderItemChoice orderItemChoice)
        {
            var bllEntity = _mapper.Map(orderItemChoice);
            _bll.OrderItemChoices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            orderItemChoice.Id = bllEntity.Id;

            return CreatedAtAction("GetOrderItemChoice",
                new {id = orderItemChoice.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                orderItemChoice);
        }

        // DELETE: api/OrderItemChoice/5
        /// <summary>
        /// Delete the order item choice
        /// </summary>
        /// <param name="id">Id for order item choice</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.OrderItemChoice>> DeleteOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound(new V1DTO.MessageDTO("OrderItemChoice not found"));
            }

            await _bll.OrderItemChoices.RemoveAsync(orderItemChoice);
            await _bll.SaveChangesAsync();

            return Ok(orderItemChoice);
        }

        private bool OrderItemChoiceExists(Guid id)
        {
            return _bll.OrderItemChoices.Exists(id);
        }
    }
}
