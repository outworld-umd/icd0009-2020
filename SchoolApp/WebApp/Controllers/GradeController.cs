using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GradeController : Controller
    {
        private readonly AppDbContext _context;

        public GradeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Grades.Include(g => g.AbsenceReason).Include(g => g.Student).Include(g => g.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            var vm = new GradeCreateEditViewModel {
                AbsenceReasons = new SelectList(_context.AbsenceReasons, nameof(AbsenceReason.Id),
                    nameof(AbsenceReason.Id)),
                Students = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName)),
                Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName))
            };
            return View(vm);
        }

        // POST: Grade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Grade.Id = Guid.NewGuid();
                _context.Add(vm.Grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.AbsenceReasons = new SelectList(_context.AbsenceReasons, nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.Students = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Student);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Teacher);
            return View(vm);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new GradeCreateEditViewModel { Grade = await _context.Grades.FindAsync(id) };
            if (vm.Grade == null)
            {
                return NotFound();
            }
            vm.AbsenceReasons = new SelectList(_context.AbsenceReasons, nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.Students = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Student);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Teacher);
            return View(vm);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GradeCreateEditViewModel vm) {
            vm.Grade.Id = id;
            if (id != vm.Grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.Grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(vm.Grade.Id))
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
            vm.AbsenceReasons = new SelectList(_context.AbsenceReasons, nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.Students = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Student);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Grade.Teacher);
            return View(vm);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
