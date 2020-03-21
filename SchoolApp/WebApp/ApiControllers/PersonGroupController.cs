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
    public class PersonGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonGroup>>> GetPersonGroups()
        {
            return await _context.PersonGroups.ToListAsync();
        }

        // GET: api/PersonGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonGroup>> GetPersonGroup(Guid id)
        {
            var personGroup = await _context.PersonGroups.FindAsync(id);

            if (personGroup == null)
            {
                return NotFound();
            }

            return personGroup;
        }

        // PUT: api/PersonGroup/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonGroup(Guid id, PersonGroup personGroup)
        {
            if (id != personGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(personGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonGroupExists(id))
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

        // POST: api/PersonGroup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonGroup>> PostPersonGroup(PersonGroup personGroup)
        {
            _context.PersonGroups.Add(personGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonGroup", new { id = personGroup.Id }, personGroup);
        }

        // DELETE: api/PersonGroup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonGroup>> DeletePersonGroup(Guid id)
        {
            var personGroup = await _context.PersonGroups.FindAsync(id);
            if (personGroup == null)
            {
                return NotFound();
            }

            _context.PersonGroups.Remove(personGroup);
            await _context.SaveChangesAsync();

            return personGroup;
        }

        private bool PersonGroupExists(Guid id)
        {
            return _context.PersonGroups.Any(e => e.Id == id);
        }
    }
}
