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
    public class WorkingHoursController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkingHoursController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkingHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkingHours>>> GetWorkingHourses()
        {
            return await _context.WorkingHourses.ToListAsync();
        }

        // GET: api/WorkingHours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkingHours>> GetWorkingHours(Guid id)
        {
            var workingHours = await _context.WorkingHourses.FindAsync(id);

            if (workingHours == null)
            {
                return NotFound();
            }

            return workingHours;
        }

        // PUT: api/WorkingHours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkingHours(Guid id, WorkingHours workingHours)
        {
            if (id != workingHours.Id)
            {
                return BadRequest();
            }

            _context.Entry(workingHours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingHoursExists(id))
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

        // POST: api/WorkingHours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkingHours>> PostWorkingHours(WorkingHours workingHours)
        {
            _context.WorkingHourses.Add(workingHours);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkingHours", new { id = workingHours.Id }, workingHours);
        }

        // DELETE: api/WorkingHours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkingHours>> DeleteWorkingHours(Guid id)
        {
            var workingHours = await _context.WorkingHourses.FindAsync(id);
            if (workingHours == null)
            {
                return NotFound();
            }

            _context.WorkingHourses.Remove(workingHours);
            await _context.SaveChangesAsync();

            return workingHours;
        }

        private bool WorkingHoursExists(Guid id)
        {
            return _context.WorkingHourses.Any(e => e.Id == id);
        }
    }
}
