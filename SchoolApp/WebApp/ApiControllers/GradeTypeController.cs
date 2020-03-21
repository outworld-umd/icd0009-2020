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
    public class GradeTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradeTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GradeType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeType>>> GetGradeTypes()
        {
            return await _context.GradeTypes.ToListAsync();
        }

        // GET: api/GradeType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeType>> GetGradeType(Guid id)
        {
            var gradeType = await _context.GradeTypes.FindAsync(id);

            if (gradeType == null)
            {
                return NotFound();
            }

            return gradeType;
        }

        // PUT: api/GradeType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradeType(Guid id, GradeType gradeType)
        {
            if (id != gradeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(gradeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeTypeExists(id))
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

        // POST: api/GradeType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GradeType>> PostGradeType(GradeType gradeType)
        {
            _context.GradeTypes.Add(gradeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradeType", new { id = gradeType.Id }, gradeType);
        }

        // DELETE: api/GradeType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GradeType>> DeleteGradeType(Guid id)
        {
            var gradeType = await _context.GradeTypes.FindAsync(id);
            if (gradeType == null)
            {
                return NotFound();
            }

            _context.GradeTypes.Remove(gradeType);
            await _context.SaveChangesAsync();

            return gradeType;
        }

        private bool GradeTypeExists(Guid id)
        {
            return _context.GradeTypes.Any(e => e.Id == id);
        }
    }
}
