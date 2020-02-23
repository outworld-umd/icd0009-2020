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
    public class FormRoleController : Controller
    {
        private readonly AppDbContext _context;

        public FormRoleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FormRole
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormRoles.ToListAsync());
        }

        // GET: FormRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formRole = await _context.FormRoles
                .FirstOrDefaultAsync(m => m.FormRoleId == id);
            if (formRole == null)
            {
                return NotFound();
            }

            return View(formRole);
        }

        // GET: FormRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormRoleId,Name")] FormRole formRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formRole);
        }

        // GET: FormRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formRole = await _context.FormRoles.FindAsync(id);
            if (formRole == null)
            {
                return NotFound();
            }
            return View(formRole);
        }

        // POST: FormRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormRoleId,Name")] FormRole formRole)
        {
            if (id != formRole.FormRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormRoleExists(formRole.FormRoleId))
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
            return View(formRole);
        }

        // GET: FormRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formRole = await _context.FormRoles
                .FirstOrDefaultAsync(m => m.FormRoleId == id);
            if (formRole == null)
            {
                return NotFound();
            }

            return View(formRole);
        }

        // POST: FormRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formRole = await _context.FormRoles.FindAsync(id);
            _context.FormRoles.Remove(formRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormRoleExists(int id)
        {
            return _context.FormRoles.Any(e => e.FormRoleId == id);
        }
    }
}
