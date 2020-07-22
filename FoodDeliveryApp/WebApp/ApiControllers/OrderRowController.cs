using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderRowController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderRowController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderRow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRow>>> GetOrderRows()
        {
            return await _context.OrderRows.ToListAsync();
        }

        // GET: api/OrderRow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRow>> GetOrderRow(Guid id)
        {
            var orderRow = await _context.OrderRows.FindAsync(id);

            if (orderRow == null)
            {
                return NotFound();
            }

            return orderRow;
        }

        // PUT: api/OrderRow/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderRow(Guid id, OrderRow orderRow)
        {
            if (id != orderRow.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderRow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderRow
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderRow>> PostOrderRow(OrderRow orderRow)
        {
            _context.OrderRows.Add(orderRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderRow", new { id = orderRow.Id }, orderRow);
        }

        // DELETE: api/OrderRow/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderRow>> DeleteOrderRow(Guid id)
        {
            var orderRow = await _context.OrderRows.FindAsync(id);
            if (orderRow == null)
            {
                return NotFound();
            }

            _context.OrderRows.Remove(orderRow);
            await _context.SaveChangesAsync();

            return orderRow;
        }

        private bool OrderRowExists(Guid id)
        {
            return _context.OrderRows.Any(e => e.Id == id);
        }
    }
}
