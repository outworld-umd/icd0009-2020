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
    public class RestaurantCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public RestaurantCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RestaurantCategories.Include(r => r.Category).Include(r => r.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RestaurantCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _context.RestaurantCategories
                .Include(r => r.Category)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }

            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
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
                _context.Add(restaurantCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantCategory.RestaurantId);
            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);
            if (restaurantCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantCategory.RestaurantId);
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
                    _context.Update(restaurantCategory);
                    await _context.SaveChangesAsync();
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", restaurantCategory.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantCategory.RestaurantId);
            return View(restaurantCategory);
        }

        // GET: RestaurantCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantCategory = await _context.RestaurantCategories
                .Include(r => r.Category)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var restaurantCategory = await _context.RestaurantCategories.FindAsync(id);
            _context.RestaurantCategories.Remove(restaurantCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantCategoryExists(Guid id)
        {
            return _context.RestaurantCategories.Any(e => e.Id == id);
        }
    }
}
