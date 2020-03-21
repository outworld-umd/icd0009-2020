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
    public class AbsenceReasonController : Controller
    {
        private readonly AppDbContext _context;

        public AbsenceReasonController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AbsenceReason
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AbsenceReasons.Include(a => a.Creator).Include(a => a.Student);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AbsenceReason/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _context.AbsenceReasons
                .Include(a => a.Creator)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceReason == null)
            {
                return NotFound();
            }

            return View(absenceReason);
        }

        // GET: AbsenceReason/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: AbsenceReason/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CreatorId,From,To,Created,Text,IsAccepted,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] AbsenceReason absenceReason)
        {
            if (ModelState.IsValid)
            {
                absenceReason.Id = Guid.NewGuid();
                _context.Add(absenceReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // GET: AbsenceReason/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _context.AbsenceReasons.FindAsync(id);
            if (absenceReason == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // POST: AbsenceReason/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StudentId,CreatorId,From,To,Created,Text,IsAccepted,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] AbsenceReason absenceReason)
        {
            if (id != absenceReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absenceReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceReasonExists(absenceReason.Id))
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
            ViewData["CreatorId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // GET: AbsenceReason/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _context.AbsenceReasons
                .Include(a => a.Creator)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceReason == null)
            {
                return NotFound();
            }

            return View(absenceReason);
        }

        // POST: AbsenceReason/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var absenceReason = await _context.AbsenceReasons.FindAsync(id);
            _context.AbsenceReasons.Remove(absenceReason);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceReasonExists(Guid id)
        {
            return _context.AbsenceReasons.Any(e => e.Id == id);
        }
    }
}
