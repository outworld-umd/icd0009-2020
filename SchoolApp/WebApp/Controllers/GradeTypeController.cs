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
    public class GradeTypeController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public GradeTypeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GradeType
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.GradeTypes.AllAsync());
        }

        // GET: GradeType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _unitOfWork.GradeTypes.FindAsync(id);
            if (gradeType == null)
            {
                return NotFound();
            }

            return View(gradeType);
        }

        // GET: GradeType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GradeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] GradeType gradeType)
        {
            if (ModelState.IsValid)
            {
                gradeType.Id = Guid.NewGuid();
                _unitOfWork.GradeTypes.Add(gradeType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gradeType);
        }

        // GET: GradeType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _unitOfWork.GradeTypes.FindAsync(id);
            if (gradeType == null)
            {
                return NotFound();
            }
            return View(gradeType);
        }

        // POST: GradeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] GradeType gradeType)
        {
            if (id != gradeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GradeTypes.Update(gradeType);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeTypeExists(gradeType.Id))
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
            return View(gradeType);
        }

        // GET: GradeType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradeType = await _unitOfWork.GradeTypes.FindAsync(id);
            if (gradeType == null)
            {
                return NotFound();
            }

            return View(gradeType);
        }

        // POST: GradeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gradeType = await _unitOfWork.GradeTypes.FindAsync(id);
            _unitOfWork.GradeTypes.Remove(gradeType);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeTypeExists(Guid id)
        {
            return _unitOfWork.GradeTypes.Any(e => e.Id == id);
        }
    }
}
