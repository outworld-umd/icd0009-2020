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
    public class FormRoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormRoleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FormRole
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormRole>>> GetFormRoles()
        {
            return await _context.FormRoles.ToListAsync();
        }

        // GET: api/FormRole/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormRole>> GetFormRole(Guid id)
        {
            var formRole = await _context.FormRoles.FindAsync(id);

            if (formRole == null)
            {
                return NotFound();
            }

            return formRole;
        }

        // PUT: api/FormRole/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormRole(Guid id, FormRole formRole)
        {
            if (id != formRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(formRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormRoleExists(id))
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

        // POST: api/FormRole
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FormRole>> PostFormRole(FormRole formRole)
        {
            _context.FormRoles.Add(formRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormRole", new { id = formRole.Id }, formRole);
        }

        // DELETE: api/FormRole/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FormRole>> DeleteFormRole(Guid id)
        {
            var formRole = await _context.FormRoles.FindAsync(id);
            if (formRole == null)
            {
                return NotFound();
            }

            _context.FormRoles.Remove(formRole);
            await _context.SaveChangesAsync();

            return formRole;
        }

        private bool FormRoleExists(Guid id)
        {
            return _context.FormRoles.Any(e => e.Id == id);
        }
    }
}
