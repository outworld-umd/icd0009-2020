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
    public class SubjectGroupController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public SubjectGroupController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: SubjectGroup
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.SubjectGroups.AllAsync());
        }

        // GET: SubjectGroup/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _unitOfWork.SubjectGroups.FindAsync(id);
            if (subjectGroup == null)
            {
                return NotFound();
            }

            return View(subjectGroup);
        }

        // GET: SubjectGroup/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_unitOfWork.Subjects.All(), "Id", "Id");
            return View();
        }

        // POST: SubjectGroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Capacity,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SubjectGroup subjectGroup)
        {
            if (ModelState.IsValid)
            {
                subjectGroup.Id = Guid.NewGuid();
                _unitOfWork.SubjectGroups.Add(subjectGroup);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_unitOfWork.Subjects.All(), "Id", "Id", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // GET: SubjectGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _unitOfWork.SubjectGroups.FindAsync(id);
            if (subjectGroup == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_unitOfWork.Subjects.All(), "Id", "Id", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // POST: SubjectGroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SubjectId,Capacity,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] SubjectGroup subjectGroup)
        {
            if (id != subjectGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.SubjectGroups.Update(subjectGroup);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectGroupExists(subjectGroup.Id))
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
            ViewData["SubjectId"] = new SelectList(_unitOfWork.Subjects.All(), "Id", "Id", subjectGroup.SubjectId);
            return View(subjectGroup);
        }

        // GET: SubjectGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectGroup = await _unitOfWork.SubjectGroups.FindAsync(id);
            if (subjectGroup == null)
            {
                return NotFound();
            }

            return View(subjectGroup);
        }

        // POST: SubjectGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var subjectGroup = await _unitOfWork.SubjectGroups.FindAsync(id);
            _unitOfWork.SubjectGroups.Remove(subjectGroup);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectGroupExists(Guid id)
        {
            return _unitOfWork.SubjectGroups.Any(e => e.Id == id);
        }
    }
}
