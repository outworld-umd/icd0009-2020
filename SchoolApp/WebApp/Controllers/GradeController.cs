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
    public class GradeController : Controller
    {
        private readonly AppDbContext _context;

        public GradeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Grades.Include(g => g.AbsenceReason).Include(g => g.Student).Include(g => g.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "FirstName");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,IsAbsent,Description,Created,AbsenceReasonId,StudentId,TeacherId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.Id = Guid.NewGuid();
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "Id", "Id", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "Id", "Id", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Value,IsAbsent,Description,Created,AbsenceReasonId,StudentId,TeacherId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
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
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "Id", "Id", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "Id", "Id", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "Id", "Id", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
