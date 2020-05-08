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

namespace WebApp.Controllers
{
    public class ItemTypesController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public ItemTypesController(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ItemTypes
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ItemTypes.AllAsync());
        }

        // GET: ItemTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _unitOfWork.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
        }

        // GET: ItemTypes/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_unitOfWork.Restaurants.All(), "Id", "Name");
            return View();
        }

        // POST: ItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsSpecial,Description,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                itemType.Id = Guid.NewGuid();
                _unitOfWork.ItemTypes.Add(itemType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Name", itemType.RestaurantId);
            return View(itemType);
        }

        // GET: ItemTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _unitOfWork.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", itemType.RestaurantId);
            return View(itemType);
        }

        // POST: ItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,IsSpecial,Description,RestaurantId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemType itemType)
        {
            if (id != itemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ItemTypes.Update(itemType);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTypeExists(itemType.Id))
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
            ViewData["RestaurantId"] = new SelectList(await _unitOfWork.Restaurants.AllAsync(), "Id", "Address", itemType.RestaurantId);
            return View(itemType);
        }

        // GET: ItemTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await _unitOfWork.ItemTypes.FindAsync(id);
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
            var itemType = await _unitOfWork.ItemTypes.FindAsync(id);
            _unitOfWork.ItemTypes.Remove(itemType);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTypeExists(Guid id)
        {
            return _unitOfWork.ItemTypes.Any(e => e.Id == id);
        }
    }
}
