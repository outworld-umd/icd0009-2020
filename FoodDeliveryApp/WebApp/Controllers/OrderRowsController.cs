using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using WebApp.ViewModels;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Controllers
{
    public class OrderRowsController : Controller
    {
        private readonly IAppBLL _bll;

        public OrderRowsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: OrderRows
        public async Task<IActionResult> Index()
        {
            return View(await _bll.OrderRows.AllAsync());
        }

        // GET: OrderRows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _bll.OrderRows.FindAsync(id);
            if (orderRow == null)
            {
                return NotFound();
            }

            return View(orderRow);
        }

        // GET: OrderRows/Create
        public IActionResult Create()
        {
            var vm = new OrderRowCreateEditViewModel {
                Items = new SelectList(_bll.Items.All(), nameof(Item.Id), nameof(Item.Name)),
                Orders = new SelectList(_bll.Orders.All(), nameof(Order.Id), nameof(Order.Id))
            };
            return View(vm);
        }

        // POST: OrderRows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderRowCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.OrderRow.Id = Guid.NewGuid();
                _bll.OrderRows.Add(vm.OrderRow);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _bll.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);
            return View(vm);
        }

        // GET: OrderRows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new OrderRowCreateEditViewModel {
                OrderRow = await _bll.OrderRows.FindAsync(id)
            };
            if (vm.OrderRow == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);
            return View(vm);
        }

        // POST: OrderRows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderRowCreateEditViewModel vm)
        {
            if (id != vm.OrderRow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.OrderRows.Update(vm.OrderRow);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderRowExists(vm.OrderRow.Id))
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
            vm.Items = new SelectList(await _bll.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.AllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);
            return View(vm);
        }

        // GET: OrderRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderRow = await _bll.OrderRows.FindAsync(id);
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
            var orderRow = await _bll.OrderRows.FindAsync(id);
            _bll.OrderRows.Remove(orderRow);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderRowExists(Guid id)
        {
            return _bll.OrderRows.Any(e => e.Id == id);
        }
    }
}
