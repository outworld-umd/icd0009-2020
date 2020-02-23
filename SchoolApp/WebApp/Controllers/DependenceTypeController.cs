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
    public class DependenceTypeController : Controller
    {
        private readonly AppDbContext _context;

        public DependenceTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DependenceType
        public async Task<IActionResult> Index()
        {
            return View(await _context.DependenceTypes.ToListAsync());
        }

        // GET: DependenceType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _context.DependenceTypes
                .FirstOrDefaultAsync(m => m.DependenceTypeId == id);
            if (dependenceType == null)
            {
                return NotFound();
            }

            return View(dependenceType);
        }

        // GET: DependenceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DependenceType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DependenceTypeId,ParentToChildName,ChildToParentName")] DependenceType dependenceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependenceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dependenceType);
        }

        // GET: DependenceType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _context.DependenceTypes.FindAsync(id);
            if (dependenceType == null)
            {
                return NotFound();
            }
            return View(dependenceType);
        }

        // POST: DependenceType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DependenceTypeId,ParentToChildName,ChildToParentName")] DependenceType dependenceType)
        {
            if (id != dependenceType.DependenceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependenceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependenceTypeExists(dependenceType.DependenceTypeId))
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
            return View(dependenceType);
        }

        // GET: DependenceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependenceType = await _context.DependenceTypes
                .FirstOrDefaultAsync(m => m.DependenceTypeId == id);
            if (dependenceType == null)
            {
                return NotFound();
            }

            return View(dependenceType);
        }

        // POST: DependenceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependenceType = await _context.DependenceTypes.FindAsync(id);
            _context.DependenceTypes.Remove(dependenceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependenceTypeExists(int id)
        {
            return _context.DependenceTypes.Any(e => e.DependenceTypeId == id);
        }
    }
}
