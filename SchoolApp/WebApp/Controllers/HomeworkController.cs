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
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectGroupId,Added,Deadline,TeacherId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                homework.Id = Guid.NewGuid();
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", homework.TeacherId);
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", homework.TeacherId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SubjectGroupId,Added,Deadline,TeacherId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Homework homework)
        {
            if (id != homework.Id)
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
                    if (!HomeworkExists(homework.Id))
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
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", homework.SubjectGroupId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", homework.TeacherId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(Guid id)
        {
            return _context.Homeworks.Any(e => e.Id == id);
        }
    }
}
