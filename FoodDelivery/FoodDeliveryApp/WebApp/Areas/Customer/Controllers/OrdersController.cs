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
using Restaurant = BLL.App.DTO.Restaurant;

namespace WebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer, Admin, Restaurant")]
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        [Authorize(Roles = "Customer, Admin, Restaurant")]
        // GET: Orders
        public async Task<IActionResult> Index()
        {

            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            if (User.IsInRole("Restaurant"))
            {
                return View(await _bll.Orders.GetAllByUserAsync(userIdTKey));
            }
            return View(await _bll.Orders.GetAllAsync(userIdTKey));
        }

        [Authorize(Roles = "Customer, Admin, Restaurant")]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            Order order = null!;
            if (User.IsInRole("Restaurant"))
            {
                order = await _bll.Orders.FirstOrDefaultAsync(id.Value);

            }
            else
            {
                order = await _bll.Orders.FirstOrDefaultAsync(id.Value, userIdTKey);
            }
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Authorize(Roles = "Customer, Admin")]
        // GET: Orders/Create
        public IActionResult Create()
        {
            var vm = new OrderCreateEditViewModel {
                Restaurants = new SelectList(_bll.Restaurants.GetAll(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name))
            };
            return View(vm);
        }
        
        [Authorize(Roles = "Customer, Admin")]
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
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        [Authorize(Roles = "Customer, Admin, Restaurant")]
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            OrderCreateEditViewModel vm = null!;
            if (User.IsInRole("Restaurant"))
            {
                vm = new OrderCreateEditViewModel {
                    Order = await _bll.Orders.FirstOrDefaultAsync(id.Value)
                };
            }
            else
            {
                vm = new OrderCreateEditViewModel {
                    Order = await _bll.Orders.FirstOrDefaultAsync(id.Value, userIdTKey)
                };
            }
            if (vm.Order == null)
            {
                return NotFound();
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        [Authorize(Roles = "Customer, Admin, Restaurant")]
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
                    vm.Order.AppUserId = User.UserGuidId();
                    await _bll.Orders.UpdateAsync(vm.Order);
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
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.Order.RestaurantId);
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var order = await _bll.Orders.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [Authorize(Roles = "Admin")]
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.Orders.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _bll.Orders.Any(e => e.Id.Equals(id));
        }
    }
}
