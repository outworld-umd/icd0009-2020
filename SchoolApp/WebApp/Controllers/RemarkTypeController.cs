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
    public class RemarkTypeController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public RemarkTypeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: RemarkType
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.RemarkTypes.AllAsync());
        }

        // GET: RemarkType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _unitOfWork.RemarkTypes.FindAsync(id);
            if (remarkType == null)
            {
                return NotFound();
            }

            return View(remarkType);
        }

        // GET: RemarkType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RemarkType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RemarkType remarkType)
        {
            if (ModelState.IsValid)
            {
                remarkType.Id = Guid.NewGuid();
                _unitOfWork.RemarkTypes.Add(remarkType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(remarkType);
        }

        // GET: RemarkType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _unitOfWork.RemarkTypes.FindAsync(id);
            if (remarkType == null)
            {
                return NotFound();
            }
            return View(remarkType);
        }

        // POST: RemarkType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RemarkType remarkType)
        {
            if (id != remarkType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.RemarkTypes.Update(remarkType);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemarkTypeExists(remarkType.Id))
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
            return View(remarkType);
        }

        // GET: RemarkType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _unitOfWork.RemarkTypes.FindAsync(id);
            if (remarkType == null)
            {
                return NotFound();
            }

            return View(remarkType);
        }

        // POST: RemarkType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var remarkType = await _unitOfWork.RemarkTypes.FindAsync(id);
            _unitOfWork.RemarkTypes.Remove(remarkType);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemarkTypeExists(Guid id)
        {
            return _unitOfWork.RemarkTypes.Any(e => e.Id == id);
        }
    }
}
