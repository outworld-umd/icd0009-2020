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
    public class RemarkTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RemarkTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RemarkType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemarkType>>> GetRemarkTypes()
        {
            return await _context.RemarkTypes.ToListAsync();
        }

        // GET: api/RemarkType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RemarkType>> GetRemarkType(Guid id)
        {
            var remarkType = await _context.RemarkTypes.FindAsync(id);

            if (remarkType == null)
            {
                return NotFound();
            }

            return remarkType;
        }

        // PUT: api/RemarkType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemarkType(Guid id, RemarkType remarkType)
        {
            if (id != remarkType.Id)
            {
                return BadRequest();
            }

            _context.Entry(remarkType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemarkTypeExists(id))
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

        // POST: api/RemarkType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RemarkType>> PostRemarkType(RemarkType remarkType)
        {
            _context.RemarkTypes.Add(remarkType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRemarkType", new { id = remarkType.Id }, remarkType);
        }

        // DELETE: api/RemarkType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RemarkType>> DeleteRemarkType(Guid id)
        {
            var remarkType = await _context.RemarkTypes.FindAsync(id);
            if (remarkType == null)
            {
                return NotFound();
            }

            _context.RemarkTypes.Remove(remarkType);
            await _context.SaveChangesAsync();

            return remarkType;
        }

        private bool RemarkTypeExists(Guid id)
        {
            return _context.RemarkTypes.Any(e => e.Id == id);
        }
    }
}
