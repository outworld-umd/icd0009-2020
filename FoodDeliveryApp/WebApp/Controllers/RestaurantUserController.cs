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
    public class RestaurantUserController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public RestaurantUserController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: RestaurantUser
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.RestaurantUsers.AllAsync());
        }

        // GET: RestaurantUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _unitOfWork.RestaurantUsers.FindAsync(id);
            if (restaurantUser == null)
            {
                return NotFound();
            }

            return View(restaurantUser);
        }

        // GET: RestaurantUser/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_unitOfWork.Users.All(), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(_unitOfWork.Restaurants.All(), "Id", "Address");
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
                _unitOfWork.RestaurantUsers.Add(restaurantUser);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _unitOfWork.Users.AllAsync(), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _unitOfWork.RestaurantUsers.FindAsync(id);
            if (restaurantUser == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(await _unitOfWork.Users.AllAsync(), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantUser.RestaurantId);
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
                    _unitOfWork.RestaurantUsers.Update(restaurantUser);
                    await _unitOfWork.SaveChangesAsync();
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
            ViewData["AppUserId"] = new SelectList(await _unitOfWork.Users.AllAsync(), "Id", "FirstName");
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", restaurantUser.RestaurantId);
            return View(restaurantUser);
        }

        // GET: RestaurantUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantUser = await _unitOfWork.RestaurantUsers.FindAsync(id);
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
            var restaurantUser = await _unitOfWork.RestaurantUsers.FindAsync(id);
            _unitOfWork.RestaurantUsers.Remove(restaurantUser);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantUserExists(Guid id)
        {
            return _unitOfWork.RestaurantUsers.Any(e => e.Id == id);
        }
    }
}
