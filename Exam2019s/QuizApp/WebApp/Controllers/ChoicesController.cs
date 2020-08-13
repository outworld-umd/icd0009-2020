using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;

namespace WebApp.Controllers
{
    public class ChoicesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ChoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Choices
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Choices.GetAllAsync());
        }

        // GET: Choices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices
                .FirstOrDefaultAsync(id.Value);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // GET: Choices/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_uow.Questions.GetAll(), "Id", "Title");
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,QuestionId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Choice choice)
        {
            if (ModelState.IsValid)
            {
                choice.Id = Guid.NewGuid();
                _uow.Choices.Add(choice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["QuestionId"] = new SelectList(await _uow.Questions.GetAllAsync(), "Id", "Title");
            return View(choice);
        }

        // GET: Choices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices.FirstOrDefaultAsync(id.Value);
            if (choice == null)
            {
                return NotFound();
            }

            ViewData["QuestionId"] =
                new SelectList((await _uow.Questions.GetAllAsync()), "Id", "Title");
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Value,QuestionId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Choice choice)
        {
            if (id != choice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Choices.UpdateAsync(choice);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.Id))
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

            ViewData["QuestionId"] =
                new SelectList((await _uow.Questions.GetAllAsync()), "Id", "Title");
            return View(choice);
        }

        // GET: Choices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _uow.Choices
                .FirstOrDefaultAsync(id.Value);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var choice = await _uow.Choices.FirstOrDefaultAsync(id);
            await _uow.Choices.RemoveAsync(choice);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoiceExists(Guid id)
        {
            return _uow.Choices.Any(e => e.Id == id);
        }
    }
}