using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LangStringsController : Controller
    {
        private readonly IAppBLL _bll;

        public LangStringsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: LangStr
        public async Task<IActionResult> Index()
        {
            return View(await _bll.LangStrings.GetAllAsync());
        }

        // GET: LangStr/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _bll.LangStrings
                .FirstOrDefaultAsync(id.Value);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // GET: LangStr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LangStr/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] LangString langString)
        {
            if (ModelState.IsValid)
            {
                langString.Id = Guid.NewGuid();
                _bll.LangStrings.Add(langString);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(langString);
        }

        // GET: LangStr/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _bll.LangStrings.FirstOrDefaultAsync(id.Value);
            if (langStr == null)
            {
                return NotFound();
            }
            return View(langStr);
        }

        // POST: LangStr/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] LangString langString)
        {
            if (id != langString.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.LangStrings.UpdateAsync(langString);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangStrExists(langString.Id))
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
            return View(langString);
        }

        // GET: LangStr/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langStr = await _bll.LangStrings
                .FirstOrDefaultAsync(id.Value);
            if (langStr == null)
            {
                return NotFound();
            }

            return View(langStr);
        }

        // POST: LangStr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.LangStrings.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool LangStrExists(Guid id)
        {
            return _bll.LangStrings.Any(e => e.Id == id);
        }
    }
}
