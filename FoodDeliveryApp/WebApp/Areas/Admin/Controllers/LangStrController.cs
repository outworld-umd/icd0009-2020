using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LangStrController : Controller
    {
        private readonly AppDbContext _context;

        public LangStrController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LangStr
        public async Task<IActionResult> Index()
        {
            return View(await _context.LangStrings.ToListAsync());
        }

        // GET: LangStr/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // GET: LangStr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LangStr/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] LangStr langStr)
        {
            if (ModelState.IsValid)
            {
                langStr.Id = Guid.NewGuid();
                _context.Add(langStr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(langStr);
        }

        // GET: LangStr/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrings.FindAsync(id);
            if (langStr == null)
            {
                return NotFound();
            }
            return View(langStr);
        }

        // POST: LangStr/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] LangStr langStr)
        {
            if (id != langStr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(langStr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangStrExists(langStr.Id))
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
            return View(langStr);
        }

        // GET: LangStr/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _context.LangStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // POST: LangStr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var langStr = await _context.LangStrings.FindAsync(id);
            _context.LangStrings.Remove(langStr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LangStrExists(Guid id)
        {
            return _context.LangStrings.Any(e => e.Id == id);
        }
    }
}
