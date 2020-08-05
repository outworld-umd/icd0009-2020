using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Controllers
{
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
            return View(await _bll.ItemOptions.GetAllAsync());
        }

        // GET: ItemOptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // GET: ItemOptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new ItemOptionCreateEditViewModel {
                ItemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, User.UserGuidId())
            };
            if (vm.ItemOption == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // GET: ItemOptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOption = await _bll.ItemOptions.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.ItemOptions.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOptionExists(Guid id)
        {
            return _bll.ItemOptions.Any(e => e.Id.Equals(id));
        }
    }
}
