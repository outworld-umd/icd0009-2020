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
    public class OrderItemChoicesController : Controller
    {
        private readonly IAppBLL _bll;

        public OrderItemChoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: OrderItemChoices
        public async Task<IActionResult> Index()
        {
            return View(await _bll.OrderItemChoices.GetAllAsync());
        }

        // GET: OrderItemChoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
                ItemChoices = new SelectList(_bll.ItemChoices.GetAll(), nameof(ItemChoice.Id), nameof(ItemChoice.Name)),
                OrderRows = new SelectList(_bll.OrderRows.GetAll(), nameof(OrderRow.Id), nameof(OrderRow.Id))
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
                _bll.OrderItemChoices.Add(vm.OrderItemChoice);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ItemChoices = new SelectList(await _bll.ItemChoices.GetAllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _bll.OrderRows.GetAllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
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
                OrderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id.Value, User.UserGuidId())
            };
            if (vm.OrderItemChoice == null)
            {
                return NotFound();
            }
            vm.ItemChoices = new SelectList(await _bll.ItemChoices.GetAllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _bll.OrderRows.GetAllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
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
                    await _bll.OrderItemChoices.UpdateAsync(vm.OrderItemChoice);
                    await _bll.SaveChangesAsync();
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
            vm.ItemChoices = new SelectList(await _bll.ItemChoices.GetAllAsync(), nameof(ItemChoice.Id), nameof(ItemChoice.Name), vm.OrderItemChoice.ItemChoiceId);
            vm.OrderRows = new SelectList(await _bll.OrderRows.GetAllAsync(), nameof(OrderRow.Id), nameof(OrderRow.Id), vm.OrderItemChoice.OrderRowId);
            return View(vm);
        }

        // GET: OrderItemChoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItemChoice = await _bll.OrderItemChoices.FirstOrDefaultAsync(id.Value, User.UserGuidId());
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
            await _bll.OrderItemChoices.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemChoiceExists(Guid id)
        {
            return _bll.OrderItemChoices.Any(e => e.Id.Equals(id));
        }
    }
}
