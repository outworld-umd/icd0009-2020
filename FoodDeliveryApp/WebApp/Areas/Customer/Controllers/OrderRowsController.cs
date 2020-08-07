using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Customer.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer, Admin")]
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
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.OrderRows.GetAllAsync(userIdTKey));
        }

        // GET: OrderRows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();

            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id.Value, userIdTKey);
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
                Items = new SelectList(_bll.Items.GetAll(), nameof(Item.Id), nameof(Item.Name)),
                Orders = new SelectList(_bll.Orders.GetAll(), nameof(Order.Id), nameof(Order.Id)),
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.GetAllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);

            return View(vm);
        }

        // GET: OrderRows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new OrderRowCreateEditViewModel
            {
                OrderRow = await _bll.OrderRows.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.OrderRow == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.GetAllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);
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
                    await _bll.OrderRows.UpdateAsync(vm.OrderRow);
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.OrderRow.ItemId);
            vm.Orders = new SelectList(await _bll.Orders.GetAllAsync(), nameof(Order.Id), nameof(Order.Id), vm.OrderRow.OrderId);
            return View(vm);
        }

        // GET: OrderRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var orderRow = await _bll.OrderRows.FirstOrDefaultAsync(id.Value, userIdTKey);
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
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.OrderRows.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderRowExists(Guid id)
        {
            return _bll.OrderRows.Any(e => e.Id.Equals(id));
        }
    }
}
