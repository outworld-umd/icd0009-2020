using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Restaurant.ViewModels;
using WebApp.ViewModels;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Areas.Restaurant.Controllers
{
    [Authorize(Roles = "Restaurant, Admin")]
    public class RestaurantCategoriesController : Controller
    {
        private readonly IAppBLL _bll;

        public RestaurantCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: RestaurantCategories
        public async Task<IActionResult> Index()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.RestaurantCategories.GetAllAsync(userIdTKey));
        }

        // GET: RestaurantCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var restaurantCategory = await _bll.RestaurantCategories.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Create
        public IActionResult Create()
        {
            var vm = new RestaurantCategoryCreateEditViewModel {
                Categories = new SelectList(_bll.Categories.GetAll(), nameof(Category.Id), nameof(Category.Name)),
                Restaurants = new SelectList(_bll.Restaurants.GetAll(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name))
            };
            return View(vm);
        }

        // POST: RestaurantCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantCategoryCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.RestaurantCategory.Id = Guid.NewGuid();
                _bll.RestaurantCategories.Add(vm.RestaurantCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Categories = new SelectList(await _bll.Categories.GetAllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantCategory.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new RestaurantCategoryCreateEditViewModel {
                RestaurantCategory = await _bll.RestaurantCategories.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.RestaurantCategory == null)
            {
                return NotFound();
            }
            vm.Categories = new SelectList(await _bll.Categories.GetAllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantCategory.RestaurantId);
            return View(vm);
        }

        // POST: RestaurantCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RestaurantCategoryCreateEditViewModel vm)
        {
            if (id != vm.RestaurantCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.RestaurantCategories.UpdateAsync(vm.RestaurantCategory);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantCategoryExists(vm.RestaurantCategory.Id))
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
            vm.Categories = new SelectList(await _bll.Categories.GetAllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.RestaurantCategory.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var restaurantCategory = await _bll.RestaurantCategories.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return View(restaurantCategory);
        }

        // POST: RestaurantCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.RestaurantCategories.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _bll.RestaurantCategories.Any(e => e.Id.Equals(id));
        }
    }
}
