using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using V1DTO=PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Saved working hours of restaurants
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkingHoursController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkingHoursMapper _mapper = new WorkingHoursMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public WorkingHoursController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/WorkingHours
        /// <summary>
        /// Get working hours for single session 
        /// </summary>
        /// <returns>working hours for session</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.WorkingHours>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.WorkingHours>>> GetWorkingHourses()
        {
            return Ok((await _bll.WorkingHourses.GetAllAsync()).Select(e => _mapper.MapWorkingHours(e)));
        }

        // GET: api/WorkingHours/5
        /// <summary>
        /// Get a single working hours of a restaurant
        /// </summary>
        /// <param name="id">id for working hours</param>
        /// <returns>working hours</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Customer, Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.WorkingHours))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.WorkingHours>> GetWorkingHours(Guid id)
        {
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id);

            if (workingHours == null)
            {
                return NotFound(new V1DTO.MessageDTO($"WorkingHours with id {id} not found"));
            }

            return Ok(_mapper.Map(workingHours));
        }

        // PUT: api/WorkingHours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update working hours
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workingHours">working hours</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutWorkingHours(Guid id, V1DTO.WorkingHours workingHours)
        {
            if (id != workingHours.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Id and WorkingHours.Id do not match"));
            }

            await _bll.WorkingHourses.UpdateAsync(_mapper.Map(workingHours));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/WorkingHours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create/add a new working hours
        /// </summary>
        /// <param name="workingHours">Working Hours info</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "Restaurant, Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.WorkingHours))]
        public async Task<ActionResult<V1DTO.WorkingHours>> PostWorkingHours(V1DTO.WorkingHours workingHours)
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
        /// <summary>
        /// Deletes the working hours
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.WorkingHours))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(Roles = "Restaurant, Admin")]
        public async Task<ActionResult<V1DTO.WorkingHours>> DeleteWorkingHours(Guid id)
        {
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id);
            if (workingHours == null)
            {
                return NotFound(new V1DTO.MessageDTO("WorkingHours not found"));
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
