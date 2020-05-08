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
    public class NutritionInfosController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public NutritionInfosController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: NutritionInfos
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.NutritionInfos.AllAsync());
        }

        // GET: NutritionInfos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionInfo = await _unitOfWork.NutritionInfos.FindAsync(id);
            if (nutritionInfo == null)
            {
                return NotFound();
            }

            return View(nutritionInfo);
        }

        // GET: NutritionInfos/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_unitOfWork.Items.All(), "Id", "Name");
            return View();
        }

        // POST: NutritionInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Amount,Unit,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] NutritionInfo nutritionInfo)
        {
            if (ModelState.IsValid)
            {
                nutritionInfo.Id = Guid.NewGuid();
                _unitOfWork.NutritionInfos.Add(nutritionInfo);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", nutritionInfo.ItemId);
            return View(nutritionInfo);
        }

        // GET: NutritionInfos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionInfo = await _unitOfWork.NutritionInfos.FindAsync(id);
            if (nutritionInfo == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", nutritionInfo.ItemId);
            return View(nutritionInfo);
        }

        // POST: NutritionInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemId,Name,Amount,Unit,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] NutritionInfo nutritionInfo)
        {
            if (id != nutritionInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.NutritionInfos.Update(nutritionInfo);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutritionInfoExists(nutritionInfo.Id))
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", nutritionInfo.ItemId);
            return View(nutritionInfo);
        }

        // GET: NutritionInfos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionInfo = await _unitOfWork.NutritionInfos.FindAsync(id);
            if (nutritionInfo == null)
            {
                return NotFound();
            }

            return View(nutritionInfo);
        }

        // POST: NutritionInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nutritionInfo = await _unitOfWork.NutritionInfos.FindAsync(id);
            _unitOfWork.NutritionInfos.Remove(nutritionInfo);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutritionInfoExists(Guid id)
        {
            return _unitOfWork.NutritionInfos.Any(e => e.Id == id);
        }
    }
}
