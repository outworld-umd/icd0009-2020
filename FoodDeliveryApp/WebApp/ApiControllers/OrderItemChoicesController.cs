using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemChoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderItemChoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderItemChoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemChoice>>> GetOrderItemChoices()
        {
            return await _context.OrderItemChoices.ToListAsync();
        }

        // GET: api/OrderItemChoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemChoice>> GetOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _context.OrderItemChoices.FindAsync(id);

            if (orderItemChoice == null)
            {
                return NotFound();
            }

            return orderItemChoice;
        }

        // PUT: api/OrderItemChoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItemChoice(Guid id, OrderItemChoice orderItemChoice)
        {
            if (id != orderItemChoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItemChoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemChoiceExists(id))
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

        // POST: api/OrderItemChoices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderItemChoice>> PostOrderItemChoice(OrderItemChoice orderItemChoice)
        {
            _context.OrderItemChoices.Add(orderItemChoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItemChoice", new { id = orderItemChoice.Id }, orderItemChoice);
        }

        // DELETE: api/OrderItemChoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItemChoice>> DeleteOrderItemChoice(Guid id)
        {
            var orderItemChoice = await _context.OrderItemChoices.FindAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }

            _context.OrderItemChoices.Remove(orderItemChoice);
            await _context.SaveChangesAsync();

            return orderItemChoice;
        }

        private bool OrderItemChoiceExists(Guid id)
        {
            return _context.OrderItemChoices.Any(e => e.Id == id);
        }
    }
}
