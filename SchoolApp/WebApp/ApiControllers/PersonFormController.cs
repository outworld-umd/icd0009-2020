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
    public class PersonFormController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonFormController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonForm>>> GetPersonForms()
        {
            return await _context.PersonForms.ToListAsync();
        }

        // GET: api/PersonForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonForm>> GetPersonForm(Guid id)
        {
            var personForm = await _context.PersonForms.FindAsync(id);

            if (personForm == null)
            {
                return NotFound();
            }

            return personForm;
        }

        // PUT: api/PersonForm/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonForm(Guid id, PersonForm personForm)
        {
            if (id != personForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(personForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonFormExists(id))
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

        // POST: api/PersonForm
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonForm>> PostPersonForm(PersonForm personForm)
        {
            _context.PersonForms.Add(personForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonForm", new { id = personForm.Id }, personForm);
        }

        // DELETE: api/PersonForm/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonForm>> DeletePersonForm(Guid id)
        {
            var personForm = await _context.PersonForms.FindAsync(id);
            if (personForm == null)
            {
                return NotFound();
            }

            _context.PersonForms.Remove(personForm);
            await _context.SaveChangesAsync();

            return personForm;
        }

        private bool PersonFormExists(Guid id)
        {
            return _context.PersonForms.Any(e => e.Id == id);
        }
    }
}
