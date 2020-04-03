using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
        private readonly IAppUnitOfWork _unitOfWork;

        public GradeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Grade
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.Grades.AllAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _unitOfWork.Grades.FindAsync(id);
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
                AbsenceReasons = new SelectList(_unitOfWork.AbsenceReasons.All(), nameof(AbsenceReason.Id),nameof(AbsenceReason.Id)),
                StudentGroups = new SelectList(_unitOfWork.PersonGroups.All(), nameof(PersonGroup.Id), nameof(PersonGroup.PersonName)),
                Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName)),
                GradeColumns = new SelectList(_unitOfWork.GradeColumns.All(), nameof(GradeColumn.Id), nameof(GradeColumn.Id))
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
                _unitOfWork.Grades.Add(vm.Grade);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.AbsenceReasons = new SelectList(_unitOfWork.AbsenceReasons.All(), nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.StudentGroups = new SelectList(_unitOfWork.PersonGroups.All(), nameof(PersonGroup.Id), nameof(PersonGroup.PersonName), vm.Grade.StudentGroup);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Grade.Teacher);
            vm.GradeColumns = new SelectList(_unitOfWork.GradeColumns.All(), nameof(GradeColumn.Id), nameof(GradeColumn.Id), vm.Grade.GradeColumn);
            return View(vm);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new GradeCreateEditViewModel { Grade = await _unitOfWork.Grades.FindAsync(id) };
            if (vm.Grade == null)
            {
                return NotFound();
            }
            vm.AbsenceReasons = new SelectList(_unitOfWork.AbsenceReasons.All(), nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.StudentGroups = new SelectList(_unitOfWork.PersonGroups.All(), nameof(PersonGroup.Id), nameof(PersonGroup.PersonName), vm.Grade.StudentGroup);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Grade.Teacher);
            vm.GradeColumns = new SelectList(_unitOfWork.GradeColumns.All(), nameof(GradeColumn.Id), nameof(GradeColumn.Id), vm.Grade.GradeColumn);
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
                    _unitOfWork.Grades.Update(vm.Grade);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.AbsenceReasons = new SelectList(_unitOfWork.AbsenceReasons.All(), nameof(AbsenceReason.Id), nameof(AbsenceReason.Id), vm.Grade.AbsenceReason);
            vm.StudentGroups = new SelectList(_unitOfWork.PersonGroups.All(), nameof(PersonGroup.Id), nameof(PersonGroup.PersonName), vm.Grade.StudentGroup);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Grade.Teacher);
            vm.GradeColumns = new SelectList(_unitOfWork.GradeColumns.All(), nameof(GradeColumn.Id), nameof(GradeColumn.Id), vm.Grade.GradeColumn);
            return View(vm);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _unitOfWork.Grades.FindAsync(id);
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
            var grade = await _unitOfWork.Grades.FindAsync(id);
            _unitOfWork.Grades.Remove(grade);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(Guid id)
        {
            return _unitOfWork.Grades.Any(e => e.Id == id);
        }
    }
}
