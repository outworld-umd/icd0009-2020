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

namespace WebApp.Controllers
{
    public class AbsenceReasonController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public AbsenceReasonController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: AbsenceReason
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.AbsenceReasons.AllAsync());
        }

        // GET: AbsenceReason/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _unitOfWork.AbsenceReasons.FindAsync(id);
            if (absenceReason == null)
            {
                return NotFound();
            }

            return View(absenceReason);
        }

        // GET: AbsenceReason/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id");
            ViewData["StudentId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id");
            return View();
        }

        // POST: AbsenceReason/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbsenceReason absenceReason)
        {
            if (ModelState.IsValid)
            {
                Console.Write(absenceReason.ToString());
                absenceReason.Id = Guid.NewGuid();
                _unitOfWork.AbsenceReasons.Add(absenceReason);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.Write(absenceReason.ToString());
            ViewData["CreatorId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // GET: AbsenceReason/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _unitOfWork.AbsenceReasons.FindAsync(id);
            if (absenceReason == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // POST: AbsenceReason/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AbsenceReason absenceReason) {
            if (id != absenceReason.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.AbsenceReasons.Update(absenceReason);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceReasonExists(absenceReason.Id))
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
            ViewData["CreatorId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.CreatorId);
            ViewData["StudentId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "Id", absenceReason.StudentId);
            return View(absenceReason);
        }

        // GET: AbsenceReason/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceReason = await _unitOfWork.AbsenceReasons.FindAsync(id);
            if (absenceReason == null)
            {
                return NotFound();
            }

            return View(absenceReason);
        }

        // POST: AbsenceReason/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var absenceReason = await _unitOfWork.AbsenceReasons.FindAsync(id);
            _unitOfWork.AbsenceReasons.Remove(absenceReason);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceReasonExists(Guid id)
        {
            return _unitOfWork.AbsenceReasons.Any(e => e.Id == id);
        }
    }
}
