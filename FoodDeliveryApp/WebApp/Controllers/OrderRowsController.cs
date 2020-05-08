using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class OrderRowsController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public OrderRowsController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: OrderRows
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.OrderRows.AllAsync());
        }

        // GET: OrderRows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _unitOfWork.OrderRows.FindAsync(id);
            if (orderRow == null)
            {
                return NotFound();
            }

            return View(orderRow);
        }

        // GET: OrderRows/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_unitOfWork.Items.All(), "Id", "Name");
            ViewData["OrderId"] = new SelectList(_unitOfWork.Orders.All(), "Id", "Address");
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
                _unitOfWork.OrderRows.Add(orderRow);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(await _unitOfWork.Orders.AllAsync(), "Id", "Address", orderRow.OrderId);
            return View(orderRow);
        }

        // GET: OrderRows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _unitOfWork.OrderRows.FindAsync(id);
            if (orderRow == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(await _unitOfWork.Orders.AllAsync(), "Id", "Address", orderRow.OrderId);
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
                    _unitOfWork.OrderRows.Update(orderRow);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", orderRow.ItemId);
            ViewData["OrderId"] = new SelectList(await _unitOfWork.Orders.AllAsync(), "Id", "Address", orderRow.OrderId);
            return View(orderRow);
        }

        // GET: OrderRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _unitOfWork.OrderRows.FindAsync(id);
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
            var orderRow = await _unitOfWork.OrderRows.FindAsync(id);
            _unitOfWork.OrderRows.Remove(orderRow);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderRowExists(Guid id)
        {
            return _unitOfWork.OrderRows.Any(e => e.Id == id);
        }
    }
}
