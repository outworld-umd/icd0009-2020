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
    public class HomeworkController : Controller
    {
        private readonly AppDbContext _context;

        public HomeworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Homeworks.Include(h => h.SubjectGroup).Include(h => h.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            var vm = new HomeworkCreateEditViewModel {
                SubjectGroups = new SelectList(_context.SubjectGroups, nameof(SubjectGroup.Id),
                    nameof(SubjectGroup.Name)),
                Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName))
            };
            return View(vm);
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeworkCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Homework.Id = Guid.NewGuid();
                _context.Add(vm.Homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.SubjectGroups = new SelectList(_context.SubjectGroups, nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Homework.TeacherId);
            return View(vm);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new HomeworkCreateEditViewModel {Homework = await _context.Homeworks.FindAsync(id)};
            if (vm.Homework == null)
            {
                return NotFound();
            }
            vm.SubjectGroups = new SelectList(_context.SubjectGroups, nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Homework.TeacherId);
            return View(vm);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HomeworkCreateEditViewModel vm) {
            vm.Homework.Id = id;
            if (id != vm.Homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.Homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(vm.Homework.Id))
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
            vm.SubjectGroups = new SelectList(_context.SubjectGroups, nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.LastName), vm.Homework.TeacherId);
            return View(vm);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homeworks
                .Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(Guid id)
        {
            return _context.Homeworks.Any(e => e.Id == id);
        }
    }
}
