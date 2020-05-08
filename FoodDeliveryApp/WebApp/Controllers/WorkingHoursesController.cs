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
            ViewData["RestaurantId"] = new SelectList(_unitOfWork.Restaurants.All(), "Id", "Address");
            return View();
        }

        // POST: WorkingHourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeekDay,OpeningTime,ClosingTime,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] WorkingHours workingHours)
        {
            if (ModelState.IsValid)
            {
                workingHours.Id = Guid.NewGuid();
                _unitOfWork.WorkingHourses.Add(workingHours);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", workingHours.RestaurantId);
            return View(workingHours);
        }

        // GET: WorkingHourses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", workingHours.RestaurantId);
            return View(workingHours);
        }

        // POST: WorkingHourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WeekDay,OpeningTime,ClosingTime,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] WorkingHours workingHours)
        {
            if (id != workingHours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.WorkingHourses.Update(workingHours);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkingHoursExists(workingHours.Id))
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
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", workingHours.RestaurantId);
            return View(workingHours);
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
