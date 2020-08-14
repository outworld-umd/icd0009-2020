using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuizSessionsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly QuizSessionMapper _mapper = new QuizSessionMapper();


        public QuizSessionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/QuizSessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizSessionView>>> GetQuizSessions()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return Ok((await _uow.QuizSessions.GetAllAsync(userIdTKey)).Select(e => _mapper.MapView(e)));
        }

        // GET: api/QuizSessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizSession>> GetQuizSession(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var quizSession = await _uow.QuizSessions.FirstOrDefaultAsync(id, userIdTKey);

            if (quizSession == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(quizSession));
        }

        // PUT: api/QuizSessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizSession(Guid id, QuizSession quizSession)
        {
            if (id != quizSession.Id)
            {
                return BadRequest();
            }

            var dalEntity = _mapper.Map(quizSession);
            dalEntity.AppUserId = User.UserGuidId();
            await _uow.QuizSessions.UpdateAsync(dalEntity);
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/QuizSessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<QuizSession>> PostQuizSession(QuizSession quizSession)
        {
            var entity = _mapper.Map(quizSession);
            entity.AppUserId = User.UserGuidId();
            _uow.QuizSessions.Add(entity);
            await _uow.SaveChangesAsync();
            Console.WriteLine(entity.Id);
            return CreatedAtAction("GetQuizSession", new {id = entity.Id}, _mapper.Map(entity));
        }

        // DELETE: api/QuizSessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizSession>> DeleteQuizSession(Guid id)
        {
            var quizSession = await _uow.QuizSessions.FirstOrDefaultAsync(id);
            if (quizSession == null)
            {
                return NotFound();
            }

            await _uow.QuizSessions.RemoveAsync(quizSession);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizSessionExists(Guid id)
        {
            return _uow.QuizSessions.Any(e => e.Id == id);
        }
    }
}