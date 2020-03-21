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
    public class RemarkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RemarkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Remark
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remark>>> GetRemarks()
        {
            return await _context.Remarks.ToListAsync();
        }

        // GET: api/Remark/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Remark>> GetRemark(Guid id)
        {
            var remark = await _context.Remarks.FindAsync(id);

            if (remark == null)
            {
                return NotFound();
            }

            return remark;
        }

        // PUT: api/Remark/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemark(Guid id, Remark remark)
        {
            if (id != remark.Id)
            {
                return BadRequest();
            }

            _context.Entry(remark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RemarkExists(id))
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

        // POST: api/Remark
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Remark>> PostRemark(Remark remark)
        {
            _context.Remarks.Add(remark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRemark", new { id = remark.Id }, remark);
        }

        // DELETE: api/Remark/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Remark>> DeleteRemark(Guid id)
        {
            var remark = await _context.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            _context.Remarks.Remove(remark);
            await _context.SaveChangesAsync();

            return remark;
        }

        private bool RemarkExists(Guid id)
        {
            return _context.Remarks.Any(e => e.Id == id);
        }
    }
}
