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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
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
            return View(await _bll.RestaurantCategories.AllAsync());
        }

        // GET: RestaurantCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _bll.RestaurantCategories.FindAsync(id);
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
                Categories = new SelectList(_bll.Categories.All(), nameof(Category.Id), nameof(Category.Name)),
                Restaurants = new SelectList(_bll.Restaurants.All(), nameof(Restaurant.Id), nameof(Restaurant.Name))
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
            vm.Categories = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.RestaurantCategory.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new RestaurantCategoryCreateEditViewModel {
                RestaurantCategory = await _bll.RestaurantCategories.FindAsync(id)
            };
            if (vm.RestaurantCategory == null)
            {
                return NotFound();
            }
            vm.Categories = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.RestaurantCategory.RestaurantId);
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
                    _bll.RestaurantCategories.Update(vm.RestaurantCategory);
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
            vm.Categories = new SelectList(await _bll.Categories.AllAsync(), nameof(Category.Id), nameof(Category.Name), vm.RestaurantCategory.CategoryId);
            vm.Restaurants = new SelectList(await _bll.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.RestaurantCategory.RestaurantId);
            return View(vm);
        }

        // GET: RestaurantCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _bll.RestaurantCategories.FindAsync(id);
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
            var restaurantCategory = await _bll.RestaurantCategories.FindAsync(id);
            _bll.RestaurantCategories.Remove(restaurantCategory);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _bll.RestaurantCategories.Any(e => e.Id == id);
        }
    }
}
