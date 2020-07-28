using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using IAppBLL = Contracts.BLL.App.IAppBLL;

namespace WebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IAppBLL _bll;
        
        public ItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Items.GetAllAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PictureLink,Price,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] BLL.App.DTO.Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _bll.Items.Add(item);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,PictureLink,Price,Description,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] BLL.App.DTO.Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.Items.UpdateAsync(item);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _bll.Items.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Items.RemoveAsync(id, User.UserGuidId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return _bll.Items.Any(e => e.Id.Equals(id));
        }
    }
}
