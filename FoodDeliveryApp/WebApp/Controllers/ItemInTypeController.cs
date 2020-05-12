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
    public class ItemInTypeController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;
        
        public ItemInTypeController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ItemInType
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ItemInTypes.AllAsync());
        }

        // GET: ItemInType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInType = await _unitOfWork.ItemInTypes.FindAsync(id);
            if (itemInType == null)
            {
                return NotFound();
            }

            return View(itemInType);
        }

        // GET: ItemInType/Create
        public IActionResult Create() {
            var vm = new ItemInTypeCreateEditViewModel {
                Items = new SelectList(_unitOfWork.Items.All(), nameof(Item.Id), nameof(Item.Name)),
                ItemTypes = new SelectList(_unitOfWork.ItemTypes.All(), nameof(ItemType.Id), nameof(ItemType.Name))
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
                _unitOfWork.ItemInTypes.Add(vm.ItemInType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
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
                ItemInType = await _unitOfWork.ItemInTypes.FindAsync(id)
            };
            if (vm.ItemInType == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
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
                    _unitOfWork.ItemInTypes.Update(vm.ItemInType);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemInType.ItemId);
            vm.ItemTypes = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), nameof(ItemType.Id), nameof(ItemType.Name), vm.ItemInType.ItemTypeId);
            return View(vm);
        }

        // GET: ItemInType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInType = await _unitOfWork.ItemInTypes.FindAsync(id);

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
            var itemInType = await _unitOfWork.ItemInTypes.FindAsync(id);
            _unitOfWork.ItemInTypes.Remove(itemInType);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInTypeExists(Guid id)
        {
            return _unitOfWork.ItemInTypes.Any(e => e.Id == id);
        }
    }
}
