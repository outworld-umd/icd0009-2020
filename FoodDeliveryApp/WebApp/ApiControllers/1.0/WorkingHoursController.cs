using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkingHoursController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkingHoursMapper _mapper = new WorkingHoursMapper();

        public WorkingHoursController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/WorkingHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkingHours>>> GetWorkingHourses()
        {
            return Ok((await _bll.WorkingHourses.GetAllAsync()).Select(e => _mapper.MapWorkingHours(e)));
        }

        // GET: api/WorkingHours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkingHours>> GetWorkingHours(Guid id)
        {
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id);

            if (workingHours == null)
            {
                return NotFound(new MessageDTO($"WorkingHours with id {id} not found"));
            }

            return Ok(_mapper.Map(workingHours));
        }

        // PUT: api/WorkingHours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkingHours(Guid id, WorkingHours workingHours)
        {
            if (id != workingHours.Id)
            {
                return BadRequest(new MessageDTO("Id and WorkingHours.Id do not match"));
            }

            await _bll.WorkingHourses.UpdateAsync(_mapper.Map(workingHours));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/WorkingHours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkingHours>> PostWorkingHours(WorkingHours workingHours)
        {
            var bllEntity = _mapper.Map(workingHours);
            _bll.WorkingHourses.Add(bllEntity);
            await _bll.SaveChangesAsync();
            workingHours.Id = bllEntity.Id;

            return CreatedAtAction("GetWorkingHours",
                new {id = workingHours.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                workingHours);
        }

        // DELETE: api/WorkingHours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkingHours>> DeleteWorkingHours(Guid id)
        {
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id);
            if (workingHours == null)
            {
                return NotFound(new MessageDTO("WorkingHours not found"));
            }

            await _bll.WorkingHourses.RemoveAsync(workingHours);
            await _bll.SaveChangesAsync();

            return Ok(workingHours);
        }

        private bool WorkingHoursExists(Guid id)
        {
            return _bll.WorkingHourses.Exists(id);
        }
    }
}
