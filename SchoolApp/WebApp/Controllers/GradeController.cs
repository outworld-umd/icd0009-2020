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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "AbsenceReasonId", "AbsenceReasonId");
            ViewData["StudentId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,Value,IsAbsent,Description,Created,AbsenceReasonId,StudentId,TeacherId")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "AbsenceReasonId", "AbsenceReasonId", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "AbsenceReasonId", "AbsenceReasonId", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.TeacherId);
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,Value,IsAbsent,Description,Created,AbsenceReasonId,StudentId,TeacherId")] Grade grade)
        {
            if (id != grade.GradeId)
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
                    if (!GradeExists(grade.GradeId))
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
            ViewData["AbsenceReasonId"] = new SelectList(_context.AbsenceReasons, "AbsenceReasonId", "AbsenceReasonId", grade.AbsenceReasonId);
            ViewData["StudentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Persons, "PersonId", "PersonId", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }
    }
}
