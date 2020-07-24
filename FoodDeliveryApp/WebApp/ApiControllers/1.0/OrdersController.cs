using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
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

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok((await _bll.Orders.GetAllAsync()).Select(e => _mapper.MapOrder(e)));
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            if (order == null)
            {
                return NotFound(new MessageDTO($"Order with id {id} not found"));
            }

            return Ok(_mapper.Map(order));
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(new MessageDTO("Id and Order.Id do not match"));
            }

            await _bll.Orders.UpdateAsync(_mapper.Map(order));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var bllEntity = _mapper.Map(order);
            _bll.Orders.Add(bllEntity);
            await _bll.SaveChangesAsync();
            order.Id = bllEntity.Id;

            return CreatedAtAction("GetOrder",
                new {id = order.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            if (order == null)
            {
                return NotFound(new MessageDTO("Order not found"));
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
