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
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _context.Remarks
                .Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // GET: Remark/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_context.Persons, "Id", "Id");
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "Id", "Id");
            ViewData["SenderId"] = new SelectList(_context.Persons, "Id", "Id");
            return View();
        }

        // POST: Remark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderId,RecipientId,Date,Text,RemarkTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Remark remark)
        {
            if (ModelState.IsValid)
            {
                remark.Id = Guid.NewGuid();
                _context.Add(remark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipientId"] = new SelectList(_context.Persons, "Id", "Id", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "Id", "Id", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["RecipientId"] = new SelectList(_context.Persons, "Id", "Id", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "Id", "Id", remark.SenderId);
            return View(remark);
        }

        // POST: Remark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SenderId,RecipientId,Date,Text,RemarkTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Remark remark)
        {
            if (id != remark.Id)
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
                    if (!RemarkExists(remark.Id))
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
            ViewData["RecipientId"] = new SelectList(_context.Persons, "Id", "Id", remark.RecipientId);
            ViewData["RemarkTypeId"] = new SelectList(_context.RemarkTypes, "Id", "Id", remark.RemarkTypeId);
            ViewData["SenderId"] = new SelectList(_context.Persons, "Id", "Id", remark.SenderId);
            return View(remark);
        }

        // GET: Remark/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var remark = await _context.Remarks
                .Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remark == null)
            {
                return NotFound();
            }

            return View(remark);
        }

        // POST: Remark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var remark = await _context.Remarks.FindAsync(id);
            _context.Remarks.Remove(remark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemarkExists(Guid id)
        {
            return _context.Remarks.Any(e => e.Id == id);
        }
    }
}
