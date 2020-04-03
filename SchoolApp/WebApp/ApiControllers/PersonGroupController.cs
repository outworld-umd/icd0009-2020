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
    public class PersonGroupController : ControllerBase
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public PersonGroupController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/PersonGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonGroupDTO>>> GetPersonGroups() 
        {
            return Ok((await _unitOfWork.PersonGroups.AllAsync()).Select(DTOFactory.GetPersonGroupDTOForList));
        }

        // GET: api/PersonGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonGroupDTO>> GetPersonGroup(Guid id) {
            var personGroup = await _unitOfWork.PersonGroups.FindAsync(id);

            if (personGroup == null)
            {
                return NotFound();
            }

            return DTOFactory.GetPersonGroupDTOSingle(personGroup);
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

            _unitOfWork.PersonGroups.SetModified(personGroup);

            try
            {
                await _unitOfWork.SaveChangesAsync();
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
            _unitOfWork.PersonGroups.Add(personGroup);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetPersonGroup", new { id = personGroup.Id }, personGroup);
        }

        // DELETE: api/PersonGroup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonGroup>> DeletePersonGroup(Guid id)
        {
            var personGroup = await _unitOfWork.PersonGroups.FindAsync(id);
            if (personGroup == null)
            {
                return NotFound();
            }

            _unitOfWork.PersonGroups.Remove(personGroup);
            await _unitOfWork.SaveChangesAsync();

            return personGroup;
        }

        private bool PersonGroupExists(Guid id)
        {
            return _unitOfWork.PersonGroups.Any(e => e.Id == id);
        }
    }
}
