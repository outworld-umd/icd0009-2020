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
    public class OrderItemChoicesController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public OrderItemChoicesController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: OrderItemChoices
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.OrderItemChoices.AllAsync());
        }

        // GET: OrderItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _unitOfWork.OrderItemChoices.FindAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }

            return View(orderItemChoice);
        }

        // GET: OrderItemChoices/Create
        public IActionResult Create()
        {
            var vm = new OrderItemChoiceCreateEditViewModel {
                ItemChoices = new SelectList(_unitOfWork.ItemChoices.All(), nameof(ItemChoice.Id), nameof(ItemChoice.Name)),
                OrderRows = new SelectList(_unitOfWork.OrderRows.All(), nameof(OrderRow.Id), nameof(OrderRow.Id))
            };
            return View(vm);
        }

        // POST: OrderItemChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderItemChoiceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.OrderItemChoice.Id = Guid.NewGuid();
                _unitOfWork.OrderItemChoices.Add(vm.OrderItemChoice);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ItemChoices = new SelectList(await _unitOfWork.ItemChoices.AllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _unitOfWork.OrderRows.AllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
            return View(vm);
        }

        // GET: OrderItemChoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new OrderItemChoiceCreateEditViewModel {
                OrderItemChoice = await _unitOfWork.OrderItemChoices.FindAsync(id)
            };
            if (vm.OrderItemChoice == null)
            {
                return NotFound();
            }
            vm.ItemChoices = new SelectList(await _unitOfWork.ItemChoices.AllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _unitOfWork.OrderRows.AllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
            return View(vm);
        }

        // POST: OrderItemChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderItemChoiceCreateEditViewModel vm)
        {
            if (id != vm.OrderItemChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.OrderItemChoices.Update(vm.OrderItemChoice);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemChoiceExists(vm.OrderItemChoice.Id))
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
            vm.ItemChoices = new SelectList(await _unitOfWork.ItemChoices.AllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _unitOfWork.OrderRows.AllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
            return View(vm);
        }

        // GET: OrderItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _unitOfWork.OrderItemChoices.FindAsync(id);
            if (orderItemChoice == null)
            {
                return NotFound();
            }

            return View(orderItemChoice);
        }

        // POST: OrderItemChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderItemChoice = await _unitOfWork.OrderItemChoices.FindAsync(id);
            _unitOfWork.OrderItemChoices.Remove(orderItemChoice);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemChoiceExists(Guid id)
        {
            return _unitOfWork.OrderItemChoices.Any(e => e.Id == id);
        }
    }
}
