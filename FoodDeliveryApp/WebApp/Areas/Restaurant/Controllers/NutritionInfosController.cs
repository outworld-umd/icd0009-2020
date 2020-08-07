using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Restaurant.ViewModels;
using WebApp.ViewModels;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Areas.Restaurant.Controllers
{
    [Authorize(Roles = "Restaurant, Admin")]
    public class NutritionInfosController : Controller
    {
        private readonly IAppBLL _bll;

        public NutritionInfosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: NutritionInfos
        public async Task<IActionResult> Index()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.NutritionInfos.GetAllAsync(userIdTKey));
        }

        // GET: NutritionInfos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var nutritionInfo = await _bll.NutritionInfos.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (nutritionInfo == null)
            {
                return NotFound();
            }

            return View(nutritionInfo);
        }

        // GET: NutritionInfos/Create
        public IActionResult Create() {
            var vm = new NutritionInfoCreateEditViewModel {
                Items = new SelectList(_bll.Items.GetAll(), nameof(Item.Id), nameof(Item.Name))
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
                _bll.NutritionInfos.Add(vm.NutritionInfo);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
            return View(vm);
        }

        // GET: NutritionInfos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new NutritionInfoCreateEditViewModel {
                NutritionInfo = await _bll.NutritionInfos.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.NutritionInfo == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
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
                    await _bll.NutritionInfos.UpdateAsync(vm.NutritionInfo);
                    await _bll.SaveChangesAsync();
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.NutritionInfo.ItemId);
            return View(vm);
        }

        // GET: NutritionInfos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var nutritionInfo = await _bll.NutritionInfos.FirstOrDefaultAsync(id.Value, userIdTKey);
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
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.NutritionInfos.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool NutritionInfoExists(Guid id)
        {
            return _bll.NutritionInfos.Any(e => e.Id.Equals(id));
        }
    }
}
