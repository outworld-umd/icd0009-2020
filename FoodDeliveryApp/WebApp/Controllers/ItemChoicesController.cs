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
    public class ItemChoicesController : Controller
    {
        private readonly AppDbContext _context;

        public ItemChoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ItemChoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ItemChoices.Include(i => i.ItemOption);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemChoice = await _context.ItemChoices
                .Include(i => i.ItemOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemChoice == null)
            {
                return NotFound();
            }

            return View(itemChoice);
        }

        // GET: ItemChoices/Create
        public IActionResult Create()
        {
            ViewData["ItemOptionId"] = new SelectList(_context.ItemOptions, "Id", "Name");
            return View();
        }

        // POST: ItemChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AdditionalPrice,ItemOptionId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemChoice itemChoice)
        {
            if (ModelState.IsValid)
            {
                itemChoice.Id = Guid.NewGuid();
                _context.Add(itemChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemOptionId"] = new SelectList(_context.ItemOptions, "Id", "Name", itemChoice.ItemOptionId);
            return View(itemChoice);
        }

        // GET: ItemChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemChoice = await _context.ItemChoices.FindAsync(id);
            if (itemChoice == null)
            {
                return NotFound();
            }
            ViewData["ItemOptionId"] = new SelectList(_context.ItemOptions, "Id", "Name", itemChoice.ItemOptionId);
            return View(itemChoice);
        }

        // POST: ItemChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,AdditionalPrice,ItemOptionId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemChoice itemChoice)
        {
            if (id != itemChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemChoiceExists(itemChoice.Id))
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
            ViewData["ItemOptionId"] = new SelectList(_context.ItemOptions, "Id", "Name", itemChoice.ItemOptionId);
            return View(itemChoice);
        }

        // GET: ItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemChoice = await _context.ItemChoices
                .Include(i => i.ItemOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemChoice == null)
            {
                return NotFound();
            }

            return View(itemChoice);
        }

        // POST: ItemChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemChoice = await _context.ItemChoices.FindAsync(id);
            _context.ItemChoices.Remove(itemChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _context.ItemChoices.Any(e => e.Id == id);
        }
    }
}
