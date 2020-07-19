using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemChoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemChoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemChoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemChoice>>> GetItemChoices()
        {
            return await _context.ItemChoices.ToListAsync();
        }

        // GET: api/ItemChoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemChoice>> GetItemChoice(Guid id)
        {
            var itemChoice = await _context.ItemChoices.FindAsync(id);

            if (itemChoice == null)
            {
                return NotFound();
            }

            return itemChoice;
        }

        // PUT: api/ItemChoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemChoice(Guid id, ItemChoice itemChoice)
        {
            if (id != itemChoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemChoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemChoiceExists(id))
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

        // POST: api/ItemChoices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemChoice>> PostItemChoice(ItemChoice itemChoice)
        {
            _context.ItemChoices.Add(itemChoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemChoice", new { id = itemChoice.Id }, itemChoice);
        }

        // DELETE: api/ItemChoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemChoice>> DeleteItemChoice(Guid id)
        {
            var itemChoice = await _context.ItemChoices.FindAsync(id);
            if (itemChoice == null)
            {
                return NotFound();
            }

            _context.ItemChoices.Remove(itemChoice);
            await _context.SaveChangesAsync();

            return itemChoice;
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _context.ItemChoices.Any(e => e.Id == id);
        }
    }
}
