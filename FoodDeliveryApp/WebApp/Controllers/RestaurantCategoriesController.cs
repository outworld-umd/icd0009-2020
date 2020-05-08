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
    public class RestaurantCategoriesController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public RestaurantCategoriesController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: RestaurantCategories
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.RestaurantCategories.AllAsync());
        }

        // GET: RestaurantCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _unitOfWork.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.Categories.All(), "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(_unitOfWork.Restaurants.All(), "Id", "Address");
            return View();
        }

        // POST: RestaurantCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantCategory restaurantCategory)
        {
            if (ModelState.IsValid)
            {
                restaurantCategory.Id = Guid.NewGuid();
                _unitOfWork.RestaurantCategories.Add(restaurantCategory);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.AllAsync(), "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantCategory.RestaurantId);
            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _unitOfWork.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.AllAsync(), "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantCategory.RestaurantId);
            return View(restaurantCategory);
        }

        // POST: RestaurantCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantCategory restaurantCategory)
        {
            if (id != restaurantCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.RestaurantCategories.Update(restaurantCategory);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantCategoryExists(restaurantCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.AllAsync(), "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantCategory.RestaurantId);
            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _unitOfWork.RestaurantCategories.FindAsync(id);
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
            var restaurantCategory = await _unitOfWork.RestaurantCategories.FindAsync(id);
            _unitOfWork.RestaurantCategories.Remove(restaurantCategory);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _unitOfWork.RestaurantCategories.Any(e => e.Id == id);
        }
    }
}
