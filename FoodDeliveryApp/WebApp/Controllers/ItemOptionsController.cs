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
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_unitOfWork.Items.All(), "Id", "Name");
            return View();
        }

        // POST: ItemOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsRequired,IsSingle,ItemId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemOption itemOption)
        {
            if (ModelState.IsValid)
            {
                itemOption.Id = Guid.NewGuid();
                _unitOfWork.ItemOptions.Add(itemOption);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemOption.ItemId);
            return View(itemOption);
        }

        // GET: ItemOptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemOption.ItemId);
            return View(itemOption);
        }

        // POST: ItemOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,IsRequired,IsSingle,ItemId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ItemOption itemOption)
        {
            if (id != itemOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ItemOptions.Update(itemOption);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemOptionExists(itemOption.Id))
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
            ViewData["ItemId"] = new SelectList(await _unitOfWork.Items.AllAsync(), "Id", "Name", itemOption.ItemId);
            return View(itemOption);
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
