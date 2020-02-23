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
    public class SubjectGroupController : Controller
    {
        private readonly AppDbContext _context;

        public SubjectGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SubjectGroup
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SubjectGroups.Include(s => s.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SubjectGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _context.SubjectGroups
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.SubjectGroupId == id);
            if (subjectGroup == null)
            {
                return NotFound();
            }

            return View(subjectGroup);
        }

        // GET: SubjectGroup/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: SubjectGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectGroupId,SubjectId,Capacity,Name")] SubjectGroup subjectGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // GET: SubjectGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _context.SubjectGroups.FindAsync(id);
            if (subjectGroup == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // POST: SubjectGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectGroupId,SubjectId,Capacity,Name")] SubjectGroup subjectGroup)
        {
            if (id != subjectGroup.SubjectGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectGroupExists(subjectGroup.SubjectGroupId))
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
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // GET: SubjectGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _context.SubjectGroups
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.SubjectGroupId == id);
            if (subjectGroup == null)
            {
                return NotFound();
            }

            return View(subjectGroup);
        }

        // POST: SubjectGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subjectGroup = await _context.SubjectGroups.FindAsync(id);
            _context.SubjectGroups.Remove(subjectGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectGroupExists(int id)
        {
            return _context.SubjectGroups.Any(e => e.SubjectGroupId == id);
        }
    }
}