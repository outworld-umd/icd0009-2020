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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class WorkingHoursesController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public WorkingHoursesController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: WorkingHourses
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.WorkingHourses.AllAsync());
        }

        // GET: WorkingHourses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingHours = await _unitOfWork.WorkingHourses.FindAsync(id);
            if (workingHours == null)
            {
                return NotFound();
            }

            return View(workingHours);
        }

        // GET: WorkingHourses/Create
        public IActionResult Create()
        {
            var vm = new WorkingHoursCreateEditViewModel {
                Restaurants = new SelectList(_unitOfWork.Restaurants.All(), nameof(Restaurant.Id), nameof(Restaurant.Name))
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
                _unitOfWork.WorkingHourses.Add(vm.WorkingHours);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Restaurants = new SelectList(await _unitOfWork.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.WorkingHours.RestaurantId);
            return View(vm);
        }

        // GET: WorkingHourses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new WorkingHoursCreateEditViewModel {
                WorkingHours = await _unitOfWork.WorkingHourses.FindAsync(id)
            };
            if (vm.WorkingHours == null)
            {
                return NotFound();
            }
            vm.Restaurants = new SelectList(await _unitOfWork.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.WorkingHours.RestaurantId);
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
                    _unitOfWork.WorkingHourses.Update(vm.WorkingHours);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.Restaurants = new SelectList(await _unitOfWork.Restaurants.AllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name), vm.WorkingHours.RestaurantId);
            return View(vm);
        }

        // GET: WorkingHourses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingHours = await _unitOfWork.WorkingHourses.FindAsync(id);
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
            var workingHours = await _unitOfWork.WorkingHourses.FindAsync(id);
            _unitOfWork.WorkingHourses.Remove(workingHours);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkingHoursExists(Guid id)
        {
            return _unitOfWork.WorkingHourses.Any(e => e.Id == id);
        }
    }
}
