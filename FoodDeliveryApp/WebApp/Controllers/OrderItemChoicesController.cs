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
    public class OrderItemChoicesController : Controller
    {
        private readonly AppDbContext _context;

        public OrderItemChoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderItemChoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderItemChoices.Include(o => o.ItemChoice).Include(o => o.OrderRow);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _context.OrderItemChoices
                .Include(o => o.ItemChoice)
                .Include(o => o.OrderRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }

            return View(orderItemChoice);
        }

        // GET: OrderItemChoices/Create
        public IActionResult Create()
        {
            ViewData["ItemChoiceId"] = new SelectList(_context.ItemChoices, "Id", "Name");
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id");
            return View();
        }

        // POST: OrderItemChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Cost,OrderRowId,ItemChoiceId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderItemChoice orderItemChoice)
        {
            if (ModelState.IsValid)
            {
                orderItemChoice.Id = Guid.NewGuid();
                _context.Add(orderItemChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemChoiceId"] = new SelectList(_context.ItemChoices, "Id", "Name", orderItemChoice.ItemChoiceId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", orderItemChoice.OrderRowId);
            return View(orderItemChoice);
        }

        // GET: OrderItemChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _context.OrderItemChoices.FindAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }
            ViewData["ItemChoiceId"] = new SelectList(_context.ItemChoices, "Id", "Name", orderItemChoice.ItemChoiceId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", orderItemChoice.OrderRowId);
            return View(orderItemChoice);
        }

        // POST: OrderItemChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,Cost,OrderRowId,ItemChoiceId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderItemChoice orderItemChoice)
        {
            if (id != orderItemChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItemChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemChoiceExists(orderItemChoice.Id))
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
            ViewData["ItemChoiceId"] = new SelectList(_context.ItemChoices, "Id", "Name", orderItemChoice.ItemChoiceId);
            ViewData["OrderRowId"] = new SelectList(_context.OrderRows, "Id", "Id", orderItemChoice.OrderRowId);
            return View(orderItemChoice);
        }

        // GET: OrderItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _context.OrderItemChoices
                .Include(o => o.ItemChoice)
                .Include(o => o.OrderRow)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }

            return View(orderItemChoice);
        }

        // POST: OrderItemChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderItemChoice = await _context.OrderItemChoices.FindAsync(id);
            _context.OrderItemChoices.Remove(orderItemChoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemChoiceExists(Guid id)
        {
            return _context.OrderItemChoices.Any(e => e.Id == id);
        }
    }
}
