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
    public class ItemOptionsController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public ItemOptionsController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ItemOptions
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ItemOptions.AllAsync());
        }

        // GET: ItemOptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOption = await _unitOfWork.ItemOptions.FindAsync(id);
            if (itemOption == null)
            {
                return NotFound();
            }

            return View(itemOption);
        }

        // GET: ItemOptions/Create
        public IActionResult Create() {
            var vm = new ItemOptionCreateEditViewModel {
                Items = new SelectList(_unitOfWork.Items.All(), nameof(Item.Id), nameof(Item.Name))
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
                _unitOfWork.ItemOptions.Add(vm.ItemOption);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
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
                ItemOption = await _unitOfWork.ItemOptions.FindAsync(id)
            };
            if (vm.ItemOption == null)
            {
                return NotFound();
            }
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
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
                    _unitOfWork.ItemOptions.Update(vm.ItemOption);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.Items = new SelectList(await _unitOfWork.Items.AllAsync(), nameof(Item.Id), nameof(Item.Name), vm.ItemOption.ItemId);
            return View(vm);
        }

        // GET: ItemOptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemOption = await _unitOfWork.ItemOptions.FindAsync(id);
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
            var itemOption = await _unitOfWork.ItemOptions.FindAsync(id);
            _unitOfWork.ItemOptions.Remove(itemOption);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOptionExists(Guid id)
        {
            return _unitOfWork.ItemOptions.Any(e => e.Id == id);
        }
    }
}
