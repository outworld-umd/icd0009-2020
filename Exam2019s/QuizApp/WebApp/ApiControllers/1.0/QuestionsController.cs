using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using PublicApi.DTO.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly QuestionMapper _mapper = new QuestionMapper();

        public QuestionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return Ok((await _uow.Questions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(question));
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            if (!await _uow.Questions.ExistsAsync(question.Id))
            {
                return NotFound();
            }
            await _uow.Questions.UpdateAsync(_mapper.Map(question));
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _uow.Questions.Add(_mapper.Map(question));
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            await _uow.Questions.RemoveAsync(question);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(Guid id)
        {
            return _uow.Questions.Any(e => e.Id == id);
        }
    }
}
