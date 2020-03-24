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
    public class FormController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public FormController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Form
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Forms.AllAsync());
        }

        // GET: Form/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _unitOfWork.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Form/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Form/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,FormNumber,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Form form)
        {
            if (ModelState.IsValid)
            {
                form.Id = Guid.NewGuid();
                _unitOfWork.Forms.Add(form);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: Form/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _unitOfWork.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        // POST: Form/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Year,FormNumber,Name,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Forms.Update(form);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            return View(form);
        }

        // GET: Form/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _unitOfWork.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Form/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var form = await _unitOfWork.Forms.FindAsync(id);
            _unitOfWork.Forms.Remove(form);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(Guid id)
        {
            return _unitOfWork.Forms.Any(e => e.Id == id);
        }
    }
}
