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
    public class DependenceController : Controller
    {
        private readonly AppDbContext _context;

        public DependenceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dependence
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Dependences.Include(d => d.Child).Include(d => d.DependenceType).Include(d => d.Parent);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Dependence/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _context.Dependences
                .Include(d => d.Child)
                .Include(d => d.DependenceType)
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // GET: Dependence/Create
        public IActionResult Create()
        {
            ViewData["ChildId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "Id", "Id");
            ViewData["ParentId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: Dependence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentId,ChildId,DependenceTypeId,From,To,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Dependence dependence)
        {
            if (ModelState.IsValid)
            {
                dependence.Id = Guid.NewGuid();
                _context.Add(dependence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "Id", "Id", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ParentId);
            return View(dependence);
        }

        // GET: Dependence/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _context.Dependences.FindAsync(id);
            if (dependence == null)
            {
                return NotFound();
            }
            ViewData["ChildId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "Id", "Id", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ParentId);
            return View(dependence);
        }

        // POST: Dependence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ParentId,ChildId,DependenceTypeId,From,To,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Dependence dependence)
        {
            if (id != dependence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependenceExists(dependence.Id))
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
            ViewData["ChildId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "Id", "Id", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "Id", "Id", dependence.ParentId);
            return View(dependence);
        }

        // GET: Dependence/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _context.Dependences
                .Include(d => d.Child)
                .Include(d => d.DependenceType)
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // POST: Dependence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dependence = await _context.Dependences.FindAsync(id);
            _context.Dependences.Remove(dependence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependenceExists(Guid id)
        {
            return _context.Dependences.Any(e => e.Id == id);
        }
    }
}
