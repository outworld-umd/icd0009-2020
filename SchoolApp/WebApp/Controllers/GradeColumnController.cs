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
    public class GradeColumnController : Controller
    {
        private readonly AppDbContext _context;

        public GradeColumnController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GradeColumn
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GradeColumns.Include(g => g.SubjectGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GradeColumn/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeColumn = await _context.GradeColumns
                .Include(g => g.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeColumn == null)
            {
                return NotFound();
            }

            return View(gradeColumn);
        }

        // GET: GradeColumn/Create
        public IActionResult Create()
        {
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id");
            return View();
        }

        // POST: GradeColumn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeTypeId,Date,LessonNumber,Theme,SubjectGroupId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] GradeColumn gradeColumn)
        {
            if (ModelState.IsValid)
            {
                gradeColumn.Id = Guid.NewGuid();
                _context.Add(gradeColumn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", gradeColumn.SubjectGroupId);
            return View(gradeColumn);
        }

        // GET: GradeColumn/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeColumn = await _context.GradeColumns.FindAsync(id);
            if (gradeColumn == null)
            {
                return NotFound();
            }
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", gradeColumn.SubjectGroupId);
            return View(gradeColumn);
        }

        // POST: GradeColumn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GradeTypeId,Date,LessonNumber,Theme,SubjectGroupId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] GradeColumn gradeColumn)
        {
            if (id != gradeColumn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeColumn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeColumnExists(gradeColumn.Id))
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
            ViewData["SubjectGroupId"] = new SelectList(_context.SubjectGroups, "Id", "Id", gradeColumn.SubjectGroupId);
            return View(gradeColumn);
        }

        // GET: GradeColumn/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeColumn = await _context.GradeColumns
                .Include(g => g.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradeColumn == null)
            {
                return NotFound();
            }

            return View(gradeColumn);
        }

        // POST: GradeColumn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gradeColumn = await _context.GradeColumns.FindAsync(id);
            _context.GradeColumns.Remove(gradeColumn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeColumnExists(Guid id)
        {
            return _context.GradeColumns.Any(e => e.Id == id);
        }
    }
}
