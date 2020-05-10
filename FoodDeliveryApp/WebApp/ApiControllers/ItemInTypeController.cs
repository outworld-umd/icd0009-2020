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
    public class ItemInTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemInTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemInType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemInType>>> GetItemInTypes()
        {
            return await _context.ItemInTypes.ToListAsync();
        }

        // GET: api/ItemInType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemInType>> GetItemInType(Guid id)
        {
            var itemInType = await _context.ItemInTypes.FindAsync(id);

            if (itemInType == null)
            {
                return NotFound();
            }

            return itemInType;
        }

        // PUT: api/ItemInType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemInType(Guid id, ItemInType itemInType)
        {
            if (id != itemInType.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemInType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemInTypeExists(id))
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

        // POST: api/ItemInType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemInType>> PostItemInType(ItemInType itemInType)
        {
            _context.ItemInTypes.Add(itemInType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemInType", new { id = itemInType.Id }, itemInType);
        }

        // DELETE: api/ItemInType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemInType>> DeleteItemInType(Guid id)
        {
            var itemInType = await _context.ItemInTypes.FindAsync(id);
            if (itemInType == null)
            {
                return NotFound();
            }

            _context.ItemInTypes.Remove(itemInType);
            await _context.SaveChangesAsync();

            return itemInType;
        }

        private bool ItemInTypeExists(Guid id)
        {
            return _context.ItemInTypes.Any(e => e.Id == id);
        }
    }
}
