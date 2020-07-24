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
    public class OrderRowController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderRowMapper _mapper = new OrderRowMapper();

        public OrderRowController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderRow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRow>>> GetOrderRows()
        {
            return Ok((await _bll.OrderRows.GetAllAsync()).Select(e => _mapper.MapOrderRow(e)));
        }

        // GET: api/OrderRow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRow>> GetOrderRow(Guid id)
        {
            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id);

            if (orderRow == null)
            {
                return NotFound(new MessageDTO($"OrderRow with id {id} not found"));
            }

            return Ok(_mapper.Map(orderRow));
        }

        // PUT: api/OrderRow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderRow(Guid id, OrderRow orderRow)
        {
            if (id != orderRow.Id)
            {
                return BadRequest(new MessageDTO("Id and OrderRow.Id do not match"));
            }

            await _bll.OrderRows.UpdateAsync(_mapper.Map(orderRow));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderRow
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderRow>> PostOrderRow(OrderRow orderRow)
        {
            var bllEntity = _mapper.Map(orderRow);
            _bll.OrderRows.Add(bllEntity);
            await _bll.SaveChangesAsync();
            orderRow.Id = bllEntity.Id;

            return CreatedAtAction("GetOrderRow",
                new {id = orderRow.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                orderRow);
        }

        // DELETE: api/OrderRow/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderRow>> DeleteOrderRow(Guid id)
        {
            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id);
            if (orderRow == null)
            {
                return NotFound(new MessageDTO("OrderRow not found"));
            }

            await _bll.OrderRows.RemoveAsync(orderRow);
            await _bll.SaveChangesAsync();

            return Ok(orderRow);
        }

        private bool OrderRowExists(Guid id)
        {
            return _bll.OrderRows.Exists(id);
        }
    }
}
