using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuizzesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public QuizzesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Quizzes.GetAllAsync());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id, QuizzesDetDelViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.Quiz = await _uow.Quizzes
                .FirstOrDefaultAsync(id.Value);
            if (vm.Quiz == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            var vm = new QuizzesCreateEditViewModel();
            return View(vm);
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizzesCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Quiz.Id = Guid.NewGuid();
                vm.Quiz.AppUserId = User.UserGuidId();
                _uow.Quizzes.Add(vm.Quiz);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new {id = vm.Quiz.Id});
            }

            return View(vm);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new QuizzesCreateEditViewModel()
            {
                Quiz = await _uow.Quizzes.FirstOrDefaultAsync(id.Value)
            };
            if (vm.Quiz == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuizzesCreateEditViewModel vm)
        {
            if (id != vm.Quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Quizzes.UpdateAsync(vm.Quiz);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(vm.Quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Details), new {id = vm.Quiz.Id});
            }

            return View(vm);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id, QuizzesDetDelViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.Quiz = await _uow.Quizzes
                .FirstOrDefaultAsync(id.Value);
            if (vm.Quiz == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);
            foreach (var question in quiz.Questions!)
            {
                if (question.CorrectChoiceId.HasValue) await _uow.Choices.RemoveAsync(question.CorrectChoiceId.Value);
                await _uow.Questions.RemoveAsync(question);
            }
            await _uow.Quizzes.RemoveAsync(quiz);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(Guid id)
        {
            return _uow.Quizzes.Any(e => e.Id == id);
        }
    }
}