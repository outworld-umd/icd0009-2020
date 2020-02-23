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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _context.Dependences
                .Include(d => d.Child)
                .Include(d => d.DependenceType)
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.DependenceId == id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // GET: Dependence/Create
        public IActionResult Create()
        {
            ViewData["ChildId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "DependenceTypeId", "DependenceTypeId");
            ViewData["ParentId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: Dependence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DependenceId,ParentId,ChildId,DependenceTypeId,From,To")] Dependence dependence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "DependenceTypeId", "DependenceTypeId", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ParentId);
            return View(dependence);
        }

        // GET: Dependence/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["ChildId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "DependenceTypeId", "DependenceTypeId", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ParentId);
            return View(dependence);
        }

        // POST: Dependence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DependenceId,ParentId,ChildId,DependenceTypeId,From,To")] Dependence dependence)
        {
            if (id != dependence.DependenceId)
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
                    if (!DependenceExists(dependence.DependenceId))
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
            ViewData["ChildId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ChildId);
            ViewData["DependenceTypeId"] = new SelectList(_context.DependenceTypes, "DependenceTypeId", "DependenceTypeId", dependence.DependenceTypeId);
            ViewData["ParentId"] = new SelectList(_context.Persons, "PersonId", "PersonId", dependence.ParentId);
            return View(dependence);
        }

        // GET: Dependence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependence = await _context.Dependences
                .Include(d => d.Child)
                .Include(d => d.DependenceType)
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.DependenceId == id);
            if (dependence == null)
            {
                return NotFound();
            }

            return View(dependence);
        }

        // POST: Dependence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependence = await _context.Dependences.FindAsync(id);
            _context.Dependences.Remove(dependence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DependenceExists(int id)
        {
            return _context.Dependences.Any(e => e.DependenceId == id);
        }
    }
}
