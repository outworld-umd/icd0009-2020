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
    public class PersonGroupController : Controller
    {
        private readonly AppDbContext _context;

        public PersonGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonGroup
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonGroups.Include(p => p.Person).Include(p => p.SubjectGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonGroup/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personGroup = await _context.PersonGroups
                .Include(p => p.Person)
                .Include(p => p.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personGroup == null)
            {
                return NotFound();
            }

            return View(personGroup);
        }

        // GET: PersonGroup/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id");
            return View();
        }

        // POST: PersonGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,SubjectGroupId,From,To,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonGroup personGroup)
        {
            if (ModelState.IsValid)
            {
                personGroup.Id = Guid.NewGuid();
                _context.Add(personGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personGroup.PersonId);
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", personGroup.SubjectGroupId);
            return View(personGroup);
        }

        // GET: PersonGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personGroup = await _context.PersonGroups.FindAsync(id);
            if (personGroup == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personGroup.PersonId);
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", personGroup.SubjectGroupId);
            return View(personGroup);
        }

        // POST: PersonGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,SubjectGroupId,From,To,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonGroup personGroup)
        {
            if (id != personGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonGroupExists(personGroup.Id))
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
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "Id", personGroup.PersonId);
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", personGroup.SubjectGroupId);
            return View(personGroup);
        }

        // GET: PersonGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personGroup = await _context.PersonGroups
                .Include(p => p.Person)
                .Include(p => p.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personGroup == null)
            {
                return NotFound();
            }

            return View(personGroup);
        }

        // POST: PersonGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personGroup = await _context.PersonGroups.FindAsync(id);
            _context.PersonGroups.Remove(personGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonGroupExists(Guid id)
        {
            return _context.PersonGroups.Any(e => e.Id == id);
        }
    }
}
