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
using WebApp.ViewModels;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Controllers
{
    public class ItemTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public ItemTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ItemTypes
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ItemTypes.GetAllAsync());
        }

        // GET: ItemTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
        }

        // GET: ItemTypes/Create
        public IActionResult Create()
        {
            var vm = new ItemTypeCreateEditViewModel {
                Restaurants = new SelectList(_bll.Restaurants.GetAll(), nameof(Restaurant.Id), nameof(Restaurant.Name))
            };
            return View(vm);
        }

        // POST: ItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ItemType.Id = Guid.NewGuid();
                _bll.ItemTypes.Add(vm.ItemType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name));
            return View(vm);
        }

        // GET: ItemTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new ItemTypeCreateEditViewModel {
                ItemType = await _bll.ItemTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId())
            };
            if (vm.ItemType == null)
            {
                return NotFound();
            }
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name));
            return View(vm);
        }

        // POST: ItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemTypeCreateEditViewModel vm)
        {
            if (id != vm.ItemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.ItemTypes.UpdateAsync(vm.ItemType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTypeExists(vm.ItemType.Id))
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
            vm.Restaurants = new SelectList(await _bll.Restaurants.GetAllAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name));
            return View(vm);
        }

        // GET: ItemTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _bll.ItemTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
        }

        // POST: ItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Addresses.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTypeExists(Guid id)
        {
            return _bll.ItemTypes.Exists(id);
        }
    }
}
