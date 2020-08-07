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
    public class ItemChoicesController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemChoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ItemChoices
        public async Task<IActionResult> Index()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.ItemChoices.GetAllAsync(userIdTKey));
        }

        // GET: ItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (itemChoice == null)
            {
                return NotFound();
            }

            return View(itemChoice);
        }

        // GET: ItemChoices/Create
        public IActionResult Create()
        {
            var vm = new ItemChoiceCreateEditViewModel {
                ItemOptions = new SelectList(_bll.ItemOptions.GetAll(), nameof(ItemOption.Id), nameof(ItemOption.Name))
            };
            return View(vm);
        }

        // POST: ItemChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemChoiceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ItemChoice.Id = Guid.NewGuid();
                _bll.ItemChoices.Add(vm.ItemChoice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ItemOptions = new SelectList(await _bll.ItemOptions.GetAllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
            return View(vm);
        }

        // GET: ItemChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new ItemChoiceCreateEditViewModel {
                ItemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.ItemChoice == null) 
            {
                return NotFound();
            }
            vm.ItemOptions = new SelectList(await _bll.ItemOptions.GetAllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
            return View(vm);
        }

        // POST: ItemChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemChoiceCreateEditViewModel vm)
        {
            if (id != vm.ItemChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.ItemChoices.UpdateAsync(vm.ItemChoice);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemChoiceExists(vm.ItemChoice.Id))
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
            vm.ItemOptions = new SelectList(await _bll.ItemOptions.GetAllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
            return View(vm);
        }

        // GET: ItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var itemChoice = await _bll.ItemChoices.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (itemChoice == null)
            {
                return NotFound();
            }

            return View(itemChoice);
        }

        // POST: ItemChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.ItemChoices.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _bll.ItemChoices.Any(e => e.Id.Equals(id));
        }
    }
}
