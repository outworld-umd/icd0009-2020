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

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemChoiceController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderItemChoiceMapper _mapper = new OrderItemChoiceMapper();

        public OrderItemChoiceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderItemChoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemChoice>>> GetOrderItemChoices()
        {
            return Ok((await _bll.OrderItemChoices.GetAllAsync()).Select(e => _mapper.MapOrderItemChoice(e)));
        }

        // GET: api/OrderItemChoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemChoice>> GetOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id);

            if (orderItemChoice == null)
            {
                return NotFound(new MessageDTO($"OrderItemChoice with id {id} not found"));
            }

            return Ok(_mapper.Map(orderItemChoice));
        }

        // PUT: api/OrderItemChoice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItemChoice(Guid id, OrderItemChoice orderItemChoice)
        {
            if (id != orderItemChoice.Id)
            {
                return BadRequest(new MessageDTO("Id and OrderItemChoice.Id do not match"));
            }

            await _bll.OrderItemChoices.UpdateAsync(_mapper.Map(orderItemChoice));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderItemChoice
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderItemChoice>> PostOrderItemChoice(OrderItemChoice orderItemChoice)
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItemChoice>> DeleteOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound(new MessageDTO("OrderItemChoice not found"));
            }

            await _bll.OrderItemChoices.RemoveAsync(orderItemChoice);
            await _bll.SaveChangesAsync();

            return Ok(orderItemChoice);
        }

        // private bool OrderItemChoiceExists(Guid id)
        // {
        //     return _bll.OrderItemChoices.Any(e => e.Id == id);
        // }
    }
}
