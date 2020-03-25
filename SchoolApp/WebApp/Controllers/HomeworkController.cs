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
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public HomeworkController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Homework
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.Homeworks.AllAsync());
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _unitOfWork.Homeworks.FindAsync(id);
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
                SubjectGroups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id),
                    nameof(SubjectGroup.Name)),
                Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName))
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
                _unitOfWork.Homeworks.Add(vm.Homework);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.SubjectGroups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Homework.TeacherId);
            return View(vm);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new HomeworkCreateEditViewModel {Homework = await _unitOfWork.Homeworks.FindAsync(id)};
            if (vm.Homework == null)
            {
                return NotFound();
            }
            vm.SubjectGroups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Homework.TeacherId);
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
                    _unitOfWork.Homeworks.Update(vm.Homework);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.SubjectGroups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Homework.SubjectGroupId);
            vm.Teachers = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.FirstLastName), vm.Homework.TeacherId);
            return View(vm);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _unitOfWork.Homeworks.FindAsync(id);
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
            var homework = await _unitOfWork.Homeworks.FindAsync(id);
            _unitOfWork.Homeworks.Remove(homework);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(Guid id)
        {
            return _unitOfWork.Homeworks.Any(e => e.Id == id);
        }
    }
}
