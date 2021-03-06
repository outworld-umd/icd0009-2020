using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Restaurant.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    [Authorize(Roles = "Restaurant, Admin")]
    public class ItemOptionsController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemOptionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ItemOptions
        public async Task<IActionResult> Index()
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            return View(await _bll.ItemOptions.GetAllByUserAsync(userIdTKey));
        }

        // GET: ItemOptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (itemOption == null)
            {
                return NotFound();
            }

            return View(itemOption);
        }

        // GET: ItemOptions/Create
        public IActionResult Create() {
            var vm = new ItemOptionCreateEditViewModel {
                Items = new SelectList(_bll.Items.GetAll(), nameof(Item.Id), nameof(Item.Name))
            };
            return View(vm);
        }

        // POST: ItemOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemOptionCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ItemOption.Id = Guid.NewGuid();
                _bll.ItemOptions.Add(vm.ItemOption);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            vm.Items = new SelectList(await _bll.Items.GetAllByUserAsync(userIdTKey), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // GET: ItemOptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new ItemOptionCreateEditViewModel {
                ItemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, userIdTKey)
            };
            if (vm.ItemOption == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.GetAllByUserAsync(userIdTKey), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // POST: ItemOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemOptionCreateEditViewModel vm)
        {
            if (id != vm.ItemOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.ItemOptions.UpdateAsync(vm.ItemOption);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemOptionExists(vm.ItemOption.Id))
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
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            vm.Items = new SelectList(await _bll.Items.GetAllByUserAsync(userIdTKey), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // GET: ItemOptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, userIdTKey);
            if (itemOption == null)
            {
                return NotFound();
            }

            return View(itemOption);
        }

        // POST: ItemOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            await _bll.ItemOptions.RemoveAsync(id, userIdTKey);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOptionExists(Guid id)
        {
            return _bll.ItemOptions.Any(e => e.Id.Equals(id));
        }
    }
}
