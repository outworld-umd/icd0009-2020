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
    public class DependenceTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DependenceTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DependenceType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DependenceType>>> GetDependenceTypes()
        {
            return await _context.DependenceTypes.ToListAsync();
        }

        // GET: api/DependenceType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DependenceType>> GetDependenceType(Guid id)
        {
            var dependenceType = await _context.DependenceTypes.FindAsync(id);

            if (dependenceType == null)
            {
                return NotFound();
            }

            return dependenceType;
        }

        // PUT: api/DependenceType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDependenceType(Guid id, DependenceType dependenceType)
        {
            if (id != dependenceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(dependenceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependenceTypeExists(id))
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

        // POST: api/DependenceType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DependenceType>> PostDependenceType(DependenceType dependenceType)
        {
            _context.DependenceTypes.Add(dependenceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependenceType", new { id = dependenceType.Id }, dependenceType);
        }

        // DELETE: api/DependenceType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DependenceType>> DeleteDependenceType(Guid id)
        {
            var dependenceType = await _context.DependenceTypes.FindAsync(id);
            if (dependenceType == null)
            {
                return NotFound();
            }

            _context.DependenceTypes.Remove(dependenceType);
            await _context.SaveChangesAsync();

            return dependenceType;
        }

        private bool DependenceTypeExists(Guid id)
        {
            return _context.DependenceTypes.Any(e => e.Id == id);
        }
    }
}
