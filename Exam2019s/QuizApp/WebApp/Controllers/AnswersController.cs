using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Domain.App.Enums;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var vm = new AnswersCreateEditViewModel
            {
                Choices = new SelectList(_uow.Choices.GetAll(), nameof(Choice.Id), nameof(Choice.Value)),
                QuizSessions = new SelectList((_uow.QuizSessions.GetAll()), nameof(QuizSession.Id),
                    nameof(QuizSession.Id)),
            };
            return View(vm);
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnswersCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Answer.Id = Guid.NewGuid();
                _uow.Answers.Add(vm.Answer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdTKey = User.IsInRole("Admin") ? null : (Guid?) User.UserGuidId();
            var vm = new AnswersCreateEditViewModel
            {
                Answer = await _uow.Answers.FirstOrDefaultAsync(id.Value)
            };
            if (vm.Answer == null)
            {
                return NotFound();
            }

            vm.Choices = new SelectList(await _uow.Choices.GetAllAsync(), nameof(Choice.Id), nameof(Choice.Value));
            vm.QuizSessions = new SelectList((await _uow.QuizSessions.GetAllAsync()), nameof(QuizSession.Id),
                nameof(QuizSession.Id));
            return View(vm);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnswersCreateEditViewModel vm)
        {
            if (id != vm.Answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Answers.UpdateAsync(vm.Answer);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(vm.Answer.Id))
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
            return View(vm);
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