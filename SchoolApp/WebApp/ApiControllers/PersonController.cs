using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO;
using PublicApi.DTO.DTOs;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public PersonController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons() {
            return Ok((await _unitOfWork.Persons.AllAsync()).Select(DTOFactory.GetPersonDTOForList));
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(Guid id) {
            var person = await _unitOfWork.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return DTOFactory.GetPersonDTOSingle(person);
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Persons.SetModified(person);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Person
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _unitOfWork.Persons.Add(person);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            var person = await _unitOfWork.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _unitOfWork.Persons.Remove(person);
            await _unitOfWork.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(Guid id)
        {
            return _unitOfWork.Persons.Any(e => e.Id == id);
        }
    }
}
