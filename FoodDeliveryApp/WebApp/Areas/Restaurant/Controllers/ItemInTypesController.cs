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
    public class ItemInTypesController : Controller
    {
        private readonly IAppBLL _bll;
        
        public ItemInTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ItemInType
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ItemInTypes.GetAllAsync());
        }

        // GET: ItemInType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (itemInType == null)
            {
                return NotFound();
            }

            return View(itemInType);
        }

        // GET: ItemInType/Create
        public  IActionResult Create() {
            var vm = new ItemInTypeCreateEditViewModel {
                Items = new SelectList(_bll.Items.GetAll(), nameof(Item.Id), nameof(Item.Name)),
                ItemTypes = new SelectList(_bll.ItemTypes.GetAll(), nameof(ItemType.Id), nameof(ItemType.Name))
            };
            return View(vm);
        }

        // POST: ItemInType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemInTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ItemInType.Id = Guid.NewGuid();
                _bll.ItemInTypes.Add(vm.ItemInType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _bll.ItemTypes.GetAllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
            return View(vm);
        }

        // GET: ItemInType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new ItemInTypeCreateEditViewModel {
                ItemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId())
            };
            if (vm.ItemInType == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _bll.ItemTypes.GetAllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
            return View(vm);
        }

        // POST: ItemInType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemInTypeCreateEditViewModel vm)
        {
            if (id != vm.ItemInType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.ItemInTypes.UpdateAsync(vm.ItemInType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInTypeExists(vm.ItemInType.Id))
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
            vm.Items = new SelectList(await _bll.Items.GetAllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _bll.ItemTypes.GetAllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
            return View(vm);
        }

        // GET: ItemInType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInType = await _bll.ItemInTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (itemInType == null)
            {
                return NotFound();
            }

            return View(itemInType);
        }

        // POST: ItemInType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ItemInTypes.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInTypeExists(Guid id)
        {
            return _bll.ItemInTypes.Any(e => e.Id.Equals(id));
        }
    }
}