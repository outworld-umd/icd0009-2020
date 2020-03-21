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
    public class DependenceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DependenceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Dependence
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dependence>>> GetDependences()
        {
            return await _context.Dependences.ToListAsync();
        }

        // GET: api/Dependence/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dependence>> GetDependence(Guid id)
        {
            var dependence = await _context.Dependences.FindAsync(id);

            if (dependence == null)
            {
                return NotFound();
            }

            return dependence;
        }

        // PUT: api/Dependence/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependence(Guid id, Dependence dependence)
        {
            if (id != dependence.Id)
            {
                return BadRequest();
            }

            _context.Entry(dependence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependenceExists(id))
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

        // POST: api/Dependence
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Dependence>> PostDependence(Dependence dependence)
        {
            _context.Dependences.Add(dependence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependence", new { id = dependence.Id }, dependence);
        }

        // DELETE: api/Dependence/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dependence>> DeleteDependence(Guid id)
        {
            var dependence = await _context.Dependences.FindAsync(id);
            if (dependence == null)
            {
                return NotFound();
            }

            _context.Dependences.Remove(dependence);
            await _context.SaveChangesAsync();

            return dependence;
        }

        private bool DependenceExists(Guid id)
        {
            return _context.Dependences.Any(e => e.Id == id);
        }
    }
}
