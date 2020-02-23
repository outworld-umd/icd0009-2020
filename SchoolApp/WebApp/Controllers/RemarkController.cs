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
    public class RemarkController : Controller
    {
        private readonly AppDbContext _context;

        public RemarkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Remark
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Remarks.Include(r => r.Recipient).Include(r => r.RemarkType).Include(r => r.Sender);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Remark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _context.Remarks
                .Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender)
                .FirstOrDefaultAsync(m => m.RemarkId == id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // GET: Remark/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "RemarkTypeId", "RemarkTypeId");
            ViewData["SenderId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: Remark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RemarkId,SenderId,RecipientId,Date,Text,RemarkTypeId")] Remark remark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(remark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipientId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "RemarkTypeId", "RemarkTypeId", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _context.Remarks.FindAsync(id);
            if (remark == null)
            {
                return NotFound();
            }
            ViewData["RecipientId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "RemarkTypeId", "RemarkTypeId", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.SenderId);
            return View(remark);
        }

        // POST: Remark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RemarkId,SenderId,RecipientId,Date,Text,RemarkTypeId")] Remark remark)
        {
            if (id != remark.RemarkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(remark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemarkExists(remark.RemarkId))
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
            ViewData["RecipientId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "RemarkTypeId", "RemarkTypeId", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "PersonId", "PersonId", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _context.Remarks
                .Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender)
                .FirstOrDefaultAsync(m => m.RemarkId == id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // POST: Remark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var remark = await _context.Remarks.FindAsync(id);
            _context.Remarks.Remove(remark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemarkExists(int id)
        {
            return _context.Remarks.Any(e => e.RemarkId == id);
        }
    }
}
