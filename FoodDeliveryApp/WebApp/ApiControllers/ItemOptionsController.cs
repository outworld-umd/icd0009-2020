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
    public class ItemOptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemOptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemOption>>> GetItemOptions()
        {
            return await _context.ItemOptions.ToListAsync();
        }

        // GET: api/ItemOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemOption>> GetItemOption(Guid id)
        {
            var itemOption = await _context.ItemOptions.FindAsync(id);

            if (itemOption == null)
            {
                return NotFound();
            }

            return itemOption;
        }

        // PUT: api/ItemOptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemOption(Guid id, ItemOption itemOption)
        {
            if (id != itemOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemOptionExists(id))
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

        // POST: api/ItemOptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemOption>> PostItemOption(ItemOption itemOption)
        {
            _context.ItemOptions.Add(itemOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemOption", new { id = itemOption.Id }, itemOption);
        }

        // DELETE: api/ItemOptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemOption>> DeleteItemOption(Guid id)
        {
            var itemOption = await _context.ItemOptions.FindAsync(id);
            if (itemOption == null)
            {
                return NotFound();
            }

            _context.ItemOptions.Remove(itemOption);
            await _context.SaveChangesAsync();

            return itemOption;
        }

        private bool ItemOptionExists(Guid id)
        {
            return _context.ItemOptions.Any(e => e.Id == id);
        }
    }
}
