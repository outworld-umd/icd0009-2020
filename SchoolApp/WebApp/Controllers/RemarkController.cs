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
    public class RemarkController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public RemarkController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Remark
        public async Task<IActionResult> Index() {
            return View(await _unitOfWork.Remarks.AllAsync());
        }

        // GET: Remark/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _unitOfWork.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // GET: Remark/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName");
            ViewData["RemarkTypeId"] = new SelectList(_unitOfWork.RemarkTypes.All(), "Id", "Id");
            ViewData["SenderId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName");
            return View();
        }

        // POST: Remark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderId,RecipientId,Date,Text,RemarkTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Remark remark)
        {
            if (ModelState.IsValid)
            {
                remark.Id = Guid.NewGuid();
                _unitOfWork.Remarks.Add(remark);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipientId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_unitOfWork.RemarkTypes.All(), "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _unitOfWork.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }
            ViewData["RecipientId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_unitOfWork.RemarkTypes.All(), "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.SenderId);
            return View(remark);
        }

        // POST: Remark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SenderId,RecipientId,Date,Text,RemarkTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Remark remark)
        {
            if (id != remark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Remarks.Update(remark);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemarkExists(remark.Id))
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
            ViewData["RecipientId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_unitOfWork.RemarkTypes.All(), "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_unitOfWork.Persons.All(), "Id", "FirstLastName", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _unitOfWork.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // POST: Remark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var remark = await _unitOfWork.Remarks.FindAsync(id);
            _unitOfWork.Remarks.Remove(remark);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemarkExists(Guid id)
        {
            return _unitOfWork.Remarks.Any(e => e.Id == id);
        }
    }
}
