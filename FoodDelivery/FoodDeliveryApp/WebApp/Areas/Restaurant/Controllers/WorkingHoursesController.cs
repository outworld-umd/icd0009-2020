using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Restaurant.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    [Authorize(Roles = "Restaurant, Admin")]
    public class WorkingHoursesController : Controller
    {
        private readonly IAppBLL _bll;

        public WorkingHoursesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: WorkingHourses
        public async Task<IActionResult> Index()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.WorkingHourses.GetAllByUserAsync(userIdTKey));
        }

        // GET: WorkingHourses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (workingHours == null)
            {
                return NotFound();
            }

            return View(workingHours);
        }

        // GET: WorkingHourses/Create
        public IActionResult Create()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new WorkingHoursCreateEditViewModel {
                Restaurants = new SelectList(_bll.Restaurants.GetAllByUser(userIdTKey), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name))
            };
            return View(vm);
        }

        // POST: WorkingHourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkingHoursCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.WorkingHours.Id = Guid.NewGuid();
                _bll.WorkingHourses.Add(vm.WorkingHours);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllByUserAsync(userIdTKey), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.WorkingHours.RestaurantId);
            return View(vm);
        }

        // GET: WorkingHourses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new WorkingHoursCreateEditViewModel {
                WorkingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.WorkingHours == null)
            {
                return NotFound();
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllByUserAsync(userIdTKey), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.WorkingHours.RestaurantId);
            return View(vm);
        }

        // POST: WorkingHourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, WorkingHoursCreateEditViewModel vm)
        {
            if (id != vm.WorkingHours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.WorkingHourses.UpdateAsync(vm.WorkingHours);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkingHoursExists(vm.WorkingHours.Id))
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
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllByUserAsync(userIdTKey), nameof(BLL.App.DTO.Restaurant.Id), nameof(BLL.App.DTO.Restaurant.Name), vm.WorkingHours.RestaurantId);
            return View(vm);
        }

        // GET: WorkingHourses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var workingHours = await _bll.WorkingHourses.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (workingHours == null)
            {
                return NotFound();
            }

            return View(workingHours);
        }

        // POST: WorkingHourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.WorkingHourses.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool WorkingHoursExists(Guid id)
        {
            return _bll.WorkingHourses.Any(e => e.Id.Equals(id));
        }
    }
}
