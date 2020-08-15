using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using PublicApi.DTO.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChoicesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ChoiceMapper _mapper = new ChoiceMapper();

        public ChoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Choices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Choice>>> GetChoices()
        {
            return Ok((await _uow.Choices.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Choices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Choice>> GetChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);

            if (choice == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(choice));
            ;
        }

        // PUT: api/Choices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChoice(Guid id, Choice choice)
        {
            if (id != choice.Id)
            {
                return BadRequest();
            }

            await _uow.Choices.UpdateAsync(_mapper.Map(choice));
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Choices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Choice>> PostChoice(Choice choice)
        {
            _uow.Choices.Add(_mapper.Map(choice));
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetChoice", new {id = choice.Id}, choice);
        }

        // DELETE: api/Choices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChoice(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);
            if (choice == null)
            {
                return NotFound();
            }

            await _uow.Choices.RemoveAsync(choice);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool ChoiceExists(Guid id)
        {
            return _uow.Choices.Any(e => e.Id == id);
        }
    }
}