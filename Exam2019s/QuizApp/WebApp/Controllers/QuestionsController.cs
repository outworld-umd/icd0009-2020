using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Domain.App.Enums;
using WebApp.ViewModels;
using QuizSession = PublicApi.DTO.QuizSession;

namespace WebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public QuestionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Questions.GetAllAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(Guid? id, QuestionDetDelViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Question = await _uow.Questions
                .FirstOrDefaultAsync(id.Value);
            if (vm.Question == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            var vm = new QuestionCreateEditViewModel
            {
                CorrectChoices = new SelectList(_uow.Choices.GetAll(), nameof(Choice.Id), nameof(Choice.Value)),
                Quizzes = new SelectList(_uow.Quizzes.GetAll(), nameof(Quiz.Id), nameof(Quiz.Title))
            };
            return View(vm);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Question.Id = Guid.NewGuid();
                _uow.Questions.Add(vm.Question);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.CorrectChoices =
                new SelectList(await _uow.Choices.GetAllAsync(), nameof(Choice.Id), nameof(Choice.Value));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new QuestionCreateEditViewModel()
            {
                Question = await _uow.Questions.FirstOrDefaultAsync(id.Value)
            };
            if (vm.Question == null)
            {
                return NotFound();
            }

            vm.CorrectChoices =
                new SelectList(await _uow.Choices.GetAllAsync(), nameof(Choice.Id), nameof(Choice.Value));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuestionCreateEditViewModel vm)
        {
            if (id != vm.Question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Questions.UpdateAsync(vm.Question);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(vm.Question.Id))
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

            vm.CorrectChoices =
                new SelectList(await _uow.Choices.GetAllAsync(), nameof(Choice.Id), nameof(Choice.Value));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(Guid? id, QuestionDetDelViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Question = await _uow.Questions
                .FirstOrDefaultAsync(id.Value);
            vm.Choice = vm.Question.Choices.FirstOrDefault();
            if (vm.Question == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultAsync(id);
            await _uow.Questions.RemoveAsync(question);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(Guid id)
        {
            return _uow.Questions.Any(e => e.Id == id);
        }
    }
}