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
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;

        public HomeworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Homeworks.Include(h => h.SubjectGroup).Include(h => h.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.HomeworkId == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "SubjectGroupId", "SubjectGroupId");
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeworkId,SubjectGroupId,Added,Deadline,TeacherId")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "SubjectGroupId", "SubjectGroupId", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", homework.TeacherId);
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "SubjectGroupId", "SubjectGroupId", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", homework.TeacherId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HomeworkId,SubjectGroupId,Added,Deadline,TeacherId")] Homework homework)
        {
            if (id != homework.HomeworkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.HomeworkId))
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
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "SubjectGroupId", "SubjectGroupId", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", homework.TeacherId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.HomeworkId == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(int id)
        {
            return _context.Homeworks.Any(e => e.HomeworkId == id);
        }
    }
}
