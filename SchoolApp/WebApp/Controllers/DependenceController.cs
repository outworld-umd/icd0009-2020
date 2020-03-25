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
    public class DependenceController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public DependenceController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Dependence
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Dependences.AllAsync());
        }

        // GET: Dependence/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _unitOfWork.Dependences.FindAsync(id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // GET: Dependence/Create
        public IActionResult Create()
        {
            var vm = new DependenceCreateEditViewModel {
                Children = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName)),
                Types = new SelectList(_unitOfWork.DependenceTypes.All(), nameof(DependenceType.Id),
                    nameof(DependenceType.ChildToParentName)),
                Parents = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName))
            };
            return View(vm);
        }

        // POST: Dependence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DependenceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Dependence.Id = Guid.NewGuid();
                _unitOfWork.Dependences.Add(vm.Dependence);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Children = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ChildId);
            vm.Types = new SelectList(_unitOfWork.DependenceTypes.All(), nameof(DependenceType.Id), nameof(DependenceType.ChildToParentName), vm.Dependence.DependenceTypeId);
            vm.Parents = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ParentId);
            return View(vm);
        }

        // GET: Dependence/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new DependenceCreateEditViewModel {Dependence = await _unitOfWork.Dependences.FindAsync(id)};
            if (vm.Dependence == null)
            {
                return NotFound();
            }
            vm.Children = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ChildId);
            vm.Types = new SelectList(_unitOfWork.DependenceTypes.All(), nameof(DependenceType.Id), nameof(DependenceType.ChildToParentName), vm.Dependence.DependenceTypeId);
            vm.Parents = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ParentId);
            return View(vm);
        }

        // POST: Dependence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DependenceCreateEditViewModel vm)
        {
            vm.Dependence.Id = id;
            if (id != vm.Dependence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Dependences.Update(vm.Dependence);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependenceExists(vm.Dependence.Id))
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
            vm.Children = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ChildId);
            vm.Types = new SelectList(_unitOfWork.DependenceTypes.All(), nameof(DependenceType.Id), nameof(DependenceType.ChildToParentName), vm.Dependence.DependenceTypeId);
            vm.Parents = new SelectList(_unitOfWork.Persons.All(), nameof(Person.Id), nameof(Person.LastName), vm.Dependence.ParentId);
            return View(vm);
        }

        // GET: Dependence/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _unitOfWork.Dependences.FindAsync(id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // POST: Dependence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dependence = await _unitOfWork.Dependences.FindAsync(id);
            _unitOfWork.Dependences.Remove(dependence);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependenceExists(Guid id)
        {
            return _unitOfWork.Dependences.Any(e => e.Id == id);
        }
    }
}
