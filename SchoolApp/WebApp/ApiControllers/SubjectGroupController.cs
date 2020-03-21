using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubjectGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SubjectGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectGroup>>> GetSubjectGroups()
        {
            return await _context.SubjectGroups.ToListAsync();
        }

        // GET: api/SubjectGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectGroup>> GetSubjectGroup(Guid id)
        {
            var subjectGroup = await _context.SubjectGroups.FindAsync(id);

            if (subjectGroup == null)
            {
                return NotFound();
            }

            return subjectGroup;
        }

        // PUT: api/SubjectGroup/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectGroup(Guid id, SubjectGroup subjectGroup)
        {
            if (id != subjectGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(subjectGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectGroupExists(id))
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

        // POST: api/SubjectGroup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SubjectGroup>> PostSubjectGroup(SubjectGroup subjectGroup)
        {
            _context.SubjectGroups.Add(subjectGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjectGroup", new { id = subjectGroup.Id }, subjectGroup);
        }

        // DELETE: api/SubjectGroup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubjectGroup>> DeleteSubjectGroup(Guid id)
        {
            var subjectGroup = await _context.SubjectGroups.FindAsync(id);
            if (subjectGroup == null)
            {
                return NotFound();
            }

            _context.SubjectGroups.Remove(subjectGroup);
            await _context.SaveChangesAsync();

            return subjectGroup;
        }

        private bool SubjectGroupExists(Guid id)
        {
            return _context.SubjectGroups.Any(e => e.Id == id);
        }
    }
}
