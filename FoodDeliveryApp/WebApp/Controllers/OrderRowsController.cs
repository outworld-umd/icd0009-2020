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
    public class OrderRowsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderRowsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderRows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.OrderRows.Include(o => o.Item).Include(o => o.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: OrderRows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _context.OrderRows
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderRow == null)
            {
                return NotFound();
            }

            return View(orderRow);
        }

        // GET: OrderRows/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Address");
            return View();
        }

        // POST: OrderRows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,OrderId,Amount,Cost,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderRow orderRow)
        {
            if (ModelState.IsValid)
            {
                orderRow.Id = Guid.NewGuid();
                _context.Add(orderRow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Address", orderRow.OrderId);
            return View(orderRow);
        }

        // GET: OrderRows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _context.OrderRows.FindAsync(id);
            if (orderRow == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Address", orderRow.OrderId);
            return View(orderRow);
        }

        // POST: OrderRows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemId,OrderId,Amount,Cost,Comment,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderRow orderRow)
        {
            if (id != orderRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderRow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderRowExists(orderRow.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Address", orderRow.OrderId);
            return View(orderRow);
        }

        // GET: OrderRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _context.OrderRows
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderRow == null)
            {
                return NotFound();
            }

            return View(orderRow);
        }

        // POST: OrderRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderRow = await _context.OrderRows.FindAsync(id);
            _context.OrderRows.Remove(orderRow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderRowExists(Guid id)
        {
            return _context.OrderRows.Any(e => e.Id == id);
        }
    }
}
