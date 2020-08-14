using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Extensions;
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
    public class AnswersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly AnswerMapper _mapper = new AnswerMapper();

        public AnswersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return Ok((await _uow.Answers.GetAllAsync(userIdTKey)).Select(e => _mapper.Map(e)));
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _uow.Answers.FirstOrDefaultAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(answer));
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            await _uow.Answers.UpdateAsync(_mapper.Map(answer));
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Answers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            _uow.Answers.Add(_mapper.Map(answer));
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new {id = answer.Id}, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _uow.Answers.FirstOrDefaultAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            await _uow.Answers.RemoveAsync(answer);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(Guid id)
        {
            return _uow.Answers.Any(e => e.Id == id);
        }
    }
}