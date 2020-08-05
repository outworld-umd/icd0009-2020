using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class TranslationController : Controller
    {
        private readonly AppDbContext _context;

        public TranslationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Translation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Translations.Include(t => t.LangStr);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Translation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations
                .Include(t => t.LangStr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translation/Create
        public IActionResult Create()
        {
            ViewData["LangStrId"] = new SelectList(_context.LangStrings, "Id", "Id");
            return View();
        }

        // POST: Translation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Culture,Value,LangStrId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                translation.Id = Guid.NewGuid();
                _context.Add(translation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LangStrId"] = new SelectList(_context.LangStrings, "Id", "Id", translation.LangStrId);
            return View(translation);
        }

        // GET: Translation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }
            ViewData["LangStrId"] = new SelectList(_context.LangStrings, "Id", "Id", translation.LangStrId);
            return View(translation);
        }

        // POST: Translation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Culture,Value,LangStrId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Translation translation)
        {
            if (id != translation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(translation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationExists(translation.Id))
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
            ViewData["LangStrId"] = new SelectList(_context.LangStrings, "Id", "Id", translation.LangStrId);
            return View(translation);
        }

        // GET: Translation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations
                .Include(t => t.LangStr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Translation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var translation = await _context.Translations.FindAsync(id);
            _context.Translations.Remove(translation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranslationExists(Guid id)
        {
            return _context.Translations.Any(e => e.Id == id);
        }
    }
}
