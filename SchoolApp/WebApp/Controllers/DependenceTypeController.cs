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
    public class DependenceTypeController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public DependenceTypeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: DependenceType
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.DependenceTypes.AllAsync());
        }

        // GET: DependenceType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _unitOfWork.DependenceTypes
                .FindAsync(id);
            if (dependenceType == null)
            {
                return NotFound();
            }

            return View(dependenceType);
        }

        // GET: DependenceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DependenceType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DependenceType dependenceType)
        {
            if (ModelState.IsValid)
            {
                dependenceType.Id = Guid.NewGuid();
                _unitOfWork.DependenceTypes.Add(dependenceType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dependenceType);
        }

        // GET: DependenceType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _unitOfWork.DependenceTypes.FindAsync(id);
            if (dependenceType == null)
            {
                return NotFound();
            }
            return View(dependenceType);
        }

        // POST: DependenceType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DependenceType dependenceType)
        {
            if (id != dependenceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DependenceTypes.Update(dependenceType);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependenceTypeExists(dependenceType.Id))
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
            return View(dependenceType);
        }

        // GET: DependenceType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _unitOfWork.DependenceTypes
                .FindAsync(id);
            if (dependenceType == null)
            {
                return NotFound();
            }

            return View(dependenceType);
        }

        // POST: DependenceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dependenceType = await _unitOfWork.DependenceTypes.FindAsync(id);
            _unitOfWork.DependenceTypes.Remove(dependenceType);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependenceTypeExists(Guid id)
        {
            return _unitOfWork.DependenceTypes.Any(e => e.Id == id);
        }
    }
}
