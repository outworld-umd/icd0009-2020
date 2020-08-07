using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantUsersController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public RestaurantUsersController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: RestaurantUser
        public async Task<IActionResult> Index()
        {
            return View(await _bll.RestaurantUsers.GetAllAsync());
        }

        // GET: RestaurantUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _bll.RestaurantUsers.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // GET: RestaurantUser/Create
        public IActionResult Create()
        {
            var vm = new RestaurantUserCreateEditViewModel {
                Users = new SelectList(_userManager.Users.ToList(), nameof(AppUser.Id), nameof(AppUser.FullName)),
                Restaurants = new SelectList(_bll.Restaurants.GetAll(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name))
            };
            return View(vm);
        }

        // POST: RestaurantUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantUserCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.RestaurantUser.Id = Guid.NewGuid();
                _bll.RestaurantUsers.Add(vm.RestaurantUser);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id), nameof(AppUser.FullName), vm.RestaurantUser.AppUserId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantUser.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new RestaurantUserCreateEditViewModel {
                RestaurantUser = await _bll.RestaurantUsers.FirstOrDefaultAsync(id.Value, User.UserGuidId())
            };
            if (vm.RestaurantUser == null)
            {
                return NotFound();
            }
            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id), nameof(AppUser.FullName), vm.RestaurantUser.AppUserId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantUser.RestaurantId);
            return View(vm);
        }

        // POST: RestaurantUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RestaurantUserCreateEditViewModel vm)
        {
            if (id != vm.RestaurantUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.RestaurantUsers.UpdateAsync(vm.RestaurantUser);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantUserExists(vm.RestaurantUser.Id))
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
            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id), nameof(AppUser.FullName), vm.RestaurantUser.AppUserId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantUser.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _bll.RestaurantUsers.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // POST: RestaurantUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.RestaurantUsers.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantUserExists(Guid id)
        {
            return _bll.RestaurantUsers.Any(e => e.Id == id);
        }
    }
}
