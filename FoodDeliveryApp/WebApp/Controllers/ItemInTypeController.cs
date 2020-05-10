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
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_unitOfWork.Items.All(), "Id", "Name");
            ViewData["ItemTypeId"] = new SelectList(_unitOfWork.ItemTypes.All(), "Id", "Name");
            return View();
        }

        // POST: ItemInType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemTypeId,ItemId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemInType itemInType)
        {
            if (ModelState.IsValid)
            {
                itemInType.Id = Guid.NewGuid();
                _unitOfWork.ItemInTypes.Add(itemInType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemInType.ItemId);
            ViewData["ItemTypeId"] = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), "Id", "Name", itemInType.ItemTypeId);
            return View(itemInType);
        }

        // GET: ItemInType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemInType.ItemId);
            ViewData["ItemTypeId"] = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), "Id", "Name", itemInType.ItemTypeId);
            return View(itemInType);
        }

        // POST: ItemInType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemTypeId,ItemId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemInType itemInType)
        {
            if (id != itemInType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ItemInTypes.Update(itemInType);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInTypeExists(itemInType.Id))
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemInType.ItemId);
            ViewData["ItemTypeId"] = new SelectList(await _unitOfWork.ItemTypes.AllAsync(), "Id", "Name", itemInType.ItemTypeId);
            return View(itemInType);
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
