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
    public class AnswersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AnswersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Answers.GetAllAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _uow.Answers
                .FirstOrDefaultAsync(id.Value);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["ChoiceId"] = new SelectList(_uow.Choices.GetAll(), "Id", "Value");
            ViewData["QuizSessionIds"] = new SelectList((_uow.QuizSessions.GetAll()), "Id",
                "Value");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChoiceId, QuizSessionId, IsCorrect,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.Id = Guid.NewGuid();
                _uow.Answers.Add(answer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ChoiceId"] = new SelectList((await _uow.Choices.GetAllAsync()), "Id",
                "Value", answer.ChoiceId);
            ViewData["QuizSessionIds"] = new SelectList((await _uow.QuizSessions.GetAllAsync()), "Id",
                "Value", answer.QuizSessionId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _uow.Answers.FirstOrDefaultAsync(id.Value);
            if (answer == null)
            {
                return NotFound();
            }

            ViewData["ChoiceId"] = new SelectList((await _uow.Choices.GetAllAsync()), "Id",
                "Value", answer.ChoiceId);
            ViewData["QuizSessionIds"] = new SelectList((await _uow.QuizSessions.GetAllAsync()), "Id",
                "Value", answer.QuizSessionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("ChoiceId, QuizSessionId, IsCorrect,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Answers.UpdateAsync(answer);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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

            ViewData["ChoiceId"] = new SelectList((await _uow.Choices.GetAllAsync()), "Id",
                "Value", answer.ChoiceId);
            ViewData["QuizSessionIds"] = new SelectList((await _uow.QuizSessions.GetAllAsync()), "Id",
                "Value", answer.QuizSessionId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _uow.Answers
                .FirstOrDefaultAsync(id.Value);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var answer = await _uow.Answers.FirstOrDefaultAsync(id);
            await _uow.Answers.RemoveAsync(answer);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(Guid id)
        {
            return _uow.Answers.Any(e => e.Id == id);
        }
    }
}