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
    public class WorkingHoursesController : Controller
    {
        private readonly AppDbContext _context;

        public WorkingHoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: WorkingHourses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WorkingHourses.Include(w => w.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: WorkingHourses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingHours = await _context.WorkingHourses
                .Include(w => w.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workingHours == null)
            {
                return NotFound();
            }

            return View(workingHours);
        }

        // GET: WorkingHourses/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
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
                _context.Add(workingHours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", workingHours.RestaurantId);
            return View(workingHours);
        }

        // GET: WorkingHourses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingHours = await _context.WorkingHourses.FindAsync(id);
            if (workingHours == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", workingHours.RestaurantId);
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
                    _context.Update(workingHours);
                    await _context.SaveChangesAsync();
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", workingHours.RestaurantId);
            return View(workingHours);
        }

        // GET: WorkingHourses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingHours = await _context.WorkingHourses
                .Include(w => w.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var workingHours = await _context.WorkingHourses.FindAsync(id);
            _context.WorkingHourses.Remove(workingHours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkingHoursExists(Guid id)
        {
            return _context.WorkingHourses.Any(e => e.Id == id);
        }
    }
}
