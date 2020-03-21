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
    public class AbsenceReasonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbsenceReasonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AbsenceReason
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbsenceReason>>> GetAbsenceReasons()
        {
            return await _context.AbsenceReasons.ToListAsync();
        }

        // GET: api/AbsenceReason/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AbsenceReason>> GetAbsenceReason(Guid id)
        {
            var absenceReason = await _context.AbsenceReasons.FindAsync(id);

            if (absenceReason == null)
            {
                return NotFound();
            }

            return absenceReason;
        }

        // PUT: api/AbsenceReason/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbsenceReason(Guid id, AbsenceReason absenceReason)
        {
            if (id != absenceReason.Id)
            {
                return BadRequest();
            }

            _context.Entry(absenceReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceReasonExists(id))
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

        // POST: api/AbsenceReason
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AbsenceReason>> PostAbsenceReason(AbsenceReason absenceReason)
        {
            _context.AbsenceReasons.Add(absenceReason);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbsenceReason", new { id = absenceReason.Id }, absenceReason);
        }

        // DELETE: api/AbsenceReason/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AbsenceReason>> DeleteAbsenceReason(Guid id)
        {
            var absenceReason = await _context.AbsenceReasons.FindAsync(id);
            if (absenceReason == null)
            {
                return NotFound();
            }

            _context.AbsenceReasons.Remove(absenceReason);
            await _context.SaveChangesAsync();

            return absenceReason;
        }

        private bool AbsenceReasonExists(Guid id)
        {
            return _context.AbsenceReasons.Any(e => e.Id == id);
        }
    }
}
