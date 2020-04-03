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
    public class GradeController : ControllerBase
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public GradeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Grade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDTO>>> GetGrades() {
            return Ok((await _unitOfWork.Grades.AllAsync()).Select(DTOFactory.GetGradeDTO));
        }

        // GET: api/Grade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDTO>> GetGrade(Guid id)
        {
            var grade = await _unitOfWork.Grades.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            return DTOFactory.GetGradeDTO(grade);
        }

        // PUT: api/Grade/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(Guid id, Grade grade)
        {
            if (id != grade.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Grades.SetModified(grade);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // POST: api/Grade
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            _unitOfWork.Grades.Add(grade);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = grade.Id }, grade);
        }

        // DELETE: api/Grade/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Grade>> DeleteGrade(Guid id)
        {
            var grade = await _unitOfWork.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _unitOfWork.Grades.Remove(grade);
            await _unitOfWork.SaveChangesAsync();

            return grade;
        }

        private bool GradeExists(Guid id)
        {
            return _unitOfWork.Grades.Any(e => e.Id == id);
        }
    }
}
