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
    public class RestaurantUserController : Controller
    {
        private readonly AppDbContext _context;

        public RestaurantUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantUser
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RestaurantUsers.Include(r => r.AppUser).Include(r => r.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RestaurantUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers
                .Include(r => r.AppUser)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // GET: RestaurantUser/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
            return View();
        }

        // POST: RestaurantUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantUser restaurantUser)
        {
            if (ModelState.IsValid)
            {
                restaurantUser.Id = Guid.NewGuid();
                _context.Add(restaurantUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", restaurantUser.AppUserId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers.FindAsync(id);
            if (restaurantUser == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", restaurantUser.AppUserId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // POST: RestaurantUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RestaurantId,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RestaurantUser restaurantUser)
        {
            if (id != restaurantUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantUserExists(restaurantUser.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", restaurantUser.AppUserId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _context.RestaurantUsers
                .Include(r => r.AppUser)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var restaurantUser = await _context.RestaurantUsers.FindAsync(id);
            _context.RestaurantUsers.Remove(restaurantUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantUserExists(Guid id)
        {
            return _context.RestaurantUsers.Any(e => e.Id == id);
        }
    }
}
