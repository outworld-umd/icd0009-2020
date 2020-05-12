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
using WebApp.ViewModels;

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
        public IActionResult Create() {
            var vm = new NutritionInfoCreateEditViewModel {
                Items = new SelectList(_unitOfWork.Items.All(), nameof(Item.Id), nameof(Item.Name))
            };
            return View(vm);
        }

        // POST: NutritionInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NutritionInfoCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.NutritionInfo.Id = Guid.NewGuid();
                _unitOfWork.NutritionInfos.Add(vm.NutritionInfo);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
            return View(vm);
        }

        // GET: NutritionInfos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new NutritionInfoCreateEditViewModel {
                NutritionInfo = await _unitOfWork.NutritionInfos.FindAsync(id)
            };
            if (vm.NutritionInfo == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
            return View(vm);
        }

        // POST: NutritionInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NutritionInfoCreateEditViewModel vm)
        {
            if (id != vm.NutritionInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.NutritionInfos.Update(vm.NutritionInfo);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutritionInfoExists(vm.NutritionInfo.Id))
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
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
            return View(vm);
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
