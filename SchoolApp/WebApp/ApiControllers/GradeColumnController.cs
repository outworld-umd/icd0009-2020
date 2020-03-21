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
    public class GradeColumnController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradeColumnController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GradeColumn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeColumn>>> GetGradeColumns()
        {
            return await _context.GradeColumns.ToListAsync();
        }

        // GET: api/GradeColumn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeColumn>> GetGradeColumn(Guid id)
        {
            var gradeColumn = await _context.GradeColumns.FindAsync(id);

            if (gradeColumn == null)
            {
                return NotFound();
            }

            return gradeColumn;
        }

        // PUT: api/GradeColumn/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradeColumn(Guid id, GradeColumn gradeColumn)
        {
            if (id != gradeColumn.Id)
            {
                return BadRequest();
            }

            _context.Entry(gradeColumn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeColumnExists(id))
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

        // POST: api/GradeColumn
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GradeColumn>> PostGradeColumn(GradeColumn gradeColumn)
        {
            _context.GradeColumns.Add(gradeColumn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradeColumn", new { id = gradeColumn.Id }, gradeColumn);
        }

        // DELETE: api/GradeColumn/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GradeColumn>> DeleteGradeColumn(Guid id)
        {
            var gradeColumn = await _context.GradeColumns.FindAsync(id);
            if (gradeColumn == null)
            {
                return NotFound();
            }

            _context.GradeColumns.Remove(gradeColumn);
            await _context.SaveChangesAsync();

            return gradeColumn;
        }

        private bool GradeColumnExists(Guid id)
        {
            return _context.GradeColumns.Any(e => e.Id == id);
        }
    }
}
