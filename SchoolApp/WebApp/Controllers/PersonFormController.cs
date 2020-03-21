using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PersonFormController : Controller
    {
        private readonly AppDbContext _context;

        public PersonFormController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonForm
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonForms.Include(p => p.Form).Include(p => p.FormRole).Include(p => p.Person);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonForm/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // GET: PersonForm/Create
        public IActionResult Create()
        {
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id");
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: PersonForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,FormRoleId,PersonId,From,To,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonForm personForm)
        {
            if (ModelState.IsValid)
            {
                personForm.Id = Guid.NewGuid();
                _context.Add(personForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", personForm.FormId);
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "Id", "Id", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personForm.PersonId);
            return View(personForm);
        }

        // GET: PersonForm/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms.FindAsync(id);
            if (personForm == null)
            {
                return NotFound();
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", personForm.FormId);
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "Id", "Id", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personForm.PersonId);
            return View(personForm);
        }

        // POST: PersonForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FormId,FormRoleId,PersonId,From,To,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonForm personForm)
        {
            if (id != personForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonFormExists(personForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormId"] = new SelectList(_context.Forms, "Id", "Id", personForm.FormId);
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "Id", "Id", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personForm.PersonId);
            return View(personForm);
        }

        // GET: PersonForm/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // POST: PersonForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personForm = await _context.PersonForms.FindAsync(id);
            _context.PersonForms.Remove(personForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonFormExists(Guid id)
        {
            return _context.PersonForms.Any(e => e.Id == id);
        }
    }
}
