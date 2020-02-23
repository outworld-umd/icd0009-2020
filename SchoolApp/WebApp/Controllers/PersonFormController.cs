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
            var appDbContext = _context.PersonForms.Include(p => p.FormRole).Include(p => p.Person);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonFormId == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // GET: PersonForm/Create
        public IActionResult Create()
        {
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "FormRoleId", "FormRoleId");
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: PersonForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonFormId,FormRoleId,PersonId,From,To")] PersonForm personForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "FormRoleId", "FormRoleId", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personForm.PersonId);
            return View(personForm);
        }

        // GET: PersonForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "FormRoleId", "FormRoleId", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personForm.PersonId);
            return View(personForm);
        }

        // POST: PersonForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonFormId,FormRoleId,PersonId,From,To")] PersonForm personForm)
        {
            if (id != personForm.PersonFormId)
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
                    if (!PersonFormExists(personForm.PersonFormId))
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
            ViewData["FormRoleId"] = new SelectList(_context.FormRoles, "FormRoleId", "FormRoleId", personForm.FormRoleId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personForm.PersonId);
            return View(personForm);
        }

        // GET: PersonForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personForm = await _context.PersonForms
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonFormId == id);
            if (personForm == null)
            {
                return NotFound();
            }

            return View(personForm);
        }

        // POST: PersonForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personForm = await _context.PersonForms.FindAsync(id);
            _context.PersonForms.Remove(personForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonFormExists(int id)
        {
            return _context.PersonForms.Any(e => e.PersonFormId == id);
        }
    }
}
