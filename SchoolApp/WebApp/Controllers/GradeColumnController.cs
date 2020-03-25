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
    public class GradeColumnController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public GradeColumnController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GradeColumn
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.GradeColumns.AllAsync());
        }

        // GET: GradeColumn/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeColumn = await _unitOfWork.GradeColumns.FindAsync(id);
            if (gradeColumn == null)
            {
                return NotFound();
            }

            return View(gradeColumn);
        }

        // GET: GradeColumn/Create
        public IActionResult Create()
        {
            var vm = new GradeColumnCreateEditViewModel();
            vm.Types = new SelectList(_unitOfWork.GradeTypes.All(), nameof(GradeType.Id), nameof(GradeType.Name));
            vm.Groups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name));
            return View(vm);
        }

        // POST: GradeColumn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeColumnCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Column.Id = Guid.NewGuid();
                _unitOfWork.GradeColumns.Add(vm.Column);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Types = new SelectList(_unitOfWork.GradeTypes.All(), nameof(GradeType.Id), nameof(GradeType.Name), vm.Column.GradeTypeId);
            vm.Groups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Column.SubjectGroupId);
            return View(vm);
        }

        // GET: GradeColumn/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new GradeColumnCreateEditViewModel {Column = await _unitOfWork.GradeColumns.FindAsync(id)};
            if (vm.Column == null)
            {
                return NotFound();
            }
            vm.Types = new SelectList(_unitOfWork.GradeTypes.All(), nameof(GradeType.Id), nameof(GradeType.Name), vm.Column.GradeTypeId);
            vm.Groups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Column.SubjectGroupId);
            return View(vm);
        }

        // POST: GradeColumn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GradeColumnCreateEditViewModel vm) {
            vm.Column.Id = id;
            if (id != vm.Column.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GradeColumns.Update(vm.Column);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeColumnExists(vm.Column.Id))
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
            vm.Types = new SelectList(_unitOfWork.GradeTypes.All(), nameof(GradeType.Id), nameof(GradeType.Name), vm.Column.GradeTypeId);
            vm.Groups = new SelectList(_unitOfWork.SubjectGroups.All(), nameof(SubjectGroup.Id), nameof(SubjectGroup.Name), vm.Column.SubjectGroupId);
            return View(vm);
        }

        // GET: GradeColumn/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeColumn = await _unitOfWork.GradeColumns.FindAsync(id);
            if (gradeColumn == null)
            {
                return NotFound();
            }

            return View(gradeColumn);
        }

        // POST: GradeColumn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gradeColumn = await _unitOfWork.GradeColumns.FindAsync(id);
            _unitOfWork.GradeColumns.Remove(gradeColumn);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeColumnExists(Guid id)
        {
            return _unitOfWork.GradeColumns.Any(e => e.Id == id);
        }
    }
}
