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
    public class ItemChoicesController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public ItemChoicesController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ItemChoices
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ItemChoices.AllAsync());
        }

        // GET: ItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemChoice = await _unitOfWork.ItemChoices.FindAsync(id);
            if (itemChoice == null)
            {
                return NotFound();
            }

            return View(itemChoice);
        }

        // GET: ItemChoices/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ItemChoiceCreateEditViewModel {
                ItemOptions = new SelectList(await _unitOfWork.ItemOptions.AllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name))
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
                _unitOfWork.ItemChoices.Add(vm.ItemChoice);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ItemOptions = new SelectList(await _unitOfWork.ItemOptions.AllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
            return View(vm);
        }

        // GET: ItemChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new ItemChoiceCreateEditViewModel {
                ItemChoice = await _unitOfWork.ItemChoices.FindAsync(id)
            };
            if (vm.ItemChoice == null) 
            {
                return NotFound();
            }
            vm.ItemOptions = new SelectList(await _unitOfWork.ItemOptions.AllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
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
                    _unitOfWork.ItemChoices.Update(vm.ItemChoice);
                    await _unitOfWork.SaveChangesAsync();
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
            vm.ItemOptions = new SelectList(await _unitOfWork.ItemOptions.AllAsync(), nameof(ItemOption.Id), nameof(ItemOption.Name), vm.ItemChoice.ItemOptionId);
            return View(vm);
        }

        // GET: ItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemChoice = await _unitOfWork.ItemChoices.FindAsync(id);
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
            var itemChoice = await _unitOfWork.ItemChoices.FindAsync(id);
            _unitOfWork.ItemChoices.Remove(itemChoice);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemChoiceExists(Guid id)
        {
            return _unitOfWork.ItemChoices.Any(e => e.Id == id);
        }
    }
}
