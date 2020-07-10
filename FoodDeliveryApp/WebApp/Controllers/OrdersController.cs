using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    // [Authorize(Roles = "AppUser")]
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Orders.AllAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            var vm = new OrderCreateEditViewModel {
                Restaurants = new SelectList(_bll.Restaurants.All(), nameof(Restaurant.Id), nameof(Restaurant.Name))
            };
            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateEditViewModel vm)
        {
            vm.Order.AppUserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                vm.Order.Id = Guid.NewGuid();
                _bll.Orders.Add(vm.Order);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new OrderCreateEditViewModel {
                Order = await _bll.Orders.FindAsync(id)
            };
            if (vm.Order == null)
            {
                return NotFound();
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderCreateEditViewModel vm)
        {
            if (id != vm.Order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Orders.Update(vm.Order);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(vm.Order.Id))
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
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _bll.Orders.FindAsync(id);
            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _bll.Orders.Any(e => e.Id == id);
        }
    }
}
