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
    public class GradeTypeController : Controller
    {
        private readonly AppDbContext _context;

        public GradeTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GradeType
        public async Task<IActionResult> Index()
        {
            return View(await _context.GradeTypes.ToListAsync());
        }

        // GET: GradeType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _context.GradeTypes
                .FirstOrDefaultAsync(m => m.GradeTypeId == id);
            if (gradeType == null)
            {
                return NotFound();
            }

            return View(gradeType);
        }

        // GET: GradeType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GradeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeTypeId,Name")] GradeType gradeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeType);
        }

        // GET: GradeType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _context.GradeTypes.FindAsync(id);
            if (gradeType == null)
            {
                return NotFound();
            }
            return View(gradeType);
        }

        // POST: GradeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeTypeId,Name")] GradeType gradeType)
        {
            if (id != gradeType.GradeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeTypeExists(gradeType.GradeTypeId))
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
            return View(gradeType);
        }

        // GET: GradeType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _context.GradeTypes
                .FirstOrDefaultAsync(m => m.GradeTypeId == id);
            if (gradeType == null)
            {
                return NotFound();
            }

            return View(gradeType);
        }

        // POST: GradeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gradeType = await _context.GradeTypes.FindAsync(id);
            _context.GradeTypes.Remove(gradeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeTypeExists(int id)
        {
            return _context.GradeTypes.Any(e => e.GradeTypeId == id);
        }
    }
}
