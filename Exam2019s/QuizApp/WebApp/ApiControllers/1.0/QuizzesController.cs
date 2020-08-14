using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Contracts.DAL.App;
using Domain.App.Enums;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PublicApi.DTO;
using PublicApi.DTO.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly QuizMapper _mapper = new QuizMapper();

        public QuizzesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Quizzes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDisplay>>> GetQuizzes()
        {
            return Ok((await _uow.Quizzes.GetAllAsync()).Where(q => q.Questions!.Count > 0).Select(e => _mapper.MapDisplay(e)));
        }
        
        // GET: api/Quizzes/Quiz
        [HttpGet("Quiz")]
        public async Task<ActionResult<IEnumerable<QuizDisplay>>> GetQuizzesOnly()
        {
            return Ok((await _uow.Quizzes.GetAllAsync()).Where(q => q.Questions!.Count > 0).Where(q => q.QuizType.Equals(QuizType.Quiz))
                .Select(e => _mapper.MapDisplay(e)));
        }
        
        // GET: api/Quizzes/Poll
        [HttpGet("Poll")]
        public async Task<ActionResult<IEnumerable<QuizDisplay>>> GetPollsOnly()
        {
            return Ok((await _uow.Quizzes.GetAllAsync()).Where(q => q.Questions!.Count > 0).Where(q => q.QuizType.Equals(QuizType.Poll))
                .Select(e => _mapper.MapDisplay(e)));
        }


        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizView>> GetQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(_mapper.MapView(quiz));
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            if (!await _uow.Quizzes.ExistsAsync(quiz.Id))
            {
                return NotFound();
            }

            var dalEntity = _mapper.Map(quiz);
            dalEntity.AppUserId = User.UserGuidId();
            await _uow.Quizzes.UpdateAsync(dalEntity);
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _uow.Quizzes.Add(_mapper.Map(quiz));
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new {id = quiz.Id}, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            await _uow.Quizzes.RemoveAsync(quiz);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(Guid id)
        {
            return _uow.Quizzes.Any(e => e.Id == id);
        }
    }
}