using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class RemarkTypeController : Controller
    {
        private readonly AppDbContext _context;

        public RemarkTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RemarkType
        public async Task<IActionResult> Index()
        {
            return View(await _context.RemarkTypes.ToListAsync());
        }

        // GET: RemarkType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _context.RemarkTypes
                .FirstOrDefaultAsync(m => m.RemarkTypeId == id);
            if (remarkType == null)
            {
                return NotFound();
            }

            return View(remarkType);
        }

        // GET: RemarkType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RemarkType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RemarkTypeId,Name")] RemarkType remarkType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(remarkType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(remarkType);
        }

        // GET: RemarkType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _context.RemarkTypes.FindAsync(id);
            if (remarkType == null)
            {
                return NotFound();
            }
            return View(remarkType);
        }

        // POST: RemarkType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RemarkTypeId,Name")] RemarkType remarkType)
        {
            if (id != remarkType.RemarkTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(remarkType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemarkTypeExists(remarkType.RemarkTypeId))
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
            return View(remarkType);
        }

        // GET: RemarkType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remarkType = await _context.RemarkTypes
                .FirstOrDefaultAsync(m => m.RemarkTypeId == id);
            if (remarkType == null)
            {
                return NotFound();
            }

            return View(remarkType);
        }

        // POST: RemarkType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var remarkType = await _context.RemarkTypes.FindAsync(id);
            _context.RemarkTypes.Remove(remarkType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemarkTypeExists(int id)
        {
            return _context.RemarkTypes.Any(e => e.RemarkTypeId == id);
        }
    }
}
