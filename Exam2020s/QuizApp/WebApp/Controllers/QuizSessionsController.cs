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
    public class QuizSessionsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public QuizSessionsController(IAppUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: QuizSessions
        public async Task<IActionResult> Index()
        {
            return View(await _uow.QuizSessions.GetAllAsync());
        }

        // GET: QuizSessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizSession = await _uow.QuizSessions
                .FirstOrDefaultAsync(id.Value);
            if (quizSession == null)
            {
                return NotFound();
            }

            return View(quizSession);
        }

        // GET: QuizSessions/Create
        public IActionResult Create()
        {
            var vm = new QuizSessionsCreateViewModel()
            {
                Users = new SelectList(_userManager.Users.ToList(), nameof(AppUser.Id), nameof(AppUser.FullName)),
                Quizzes = new SelectList(_uow.Quizzes.GetAll(), nameof(Quiz.Id), nameof(Quiz.Title))
            };
            return View(vm);
        }

        // POST: QuizSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizSessionsCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.QuizSession.Id = Guid.NewGuid();
                _uow.QuizSessions.Add(vm.QuizSession);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id),
                nameof(AppUser.FullName));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // GET: QuizSessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new QuizSessionsCreateViewModel()
            {
                QuizSession = await _uow.QuizSessions.FirstOrDefaultAsync(id.Value)
            };

            if (vm.QuizSession == null)
            {
                return NotFound();
            }

            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id),
                nameof(AppUser.FullName));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // POST: QuizSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, QuizSessionsCreateViewModel vm)
        {
            if (id != vm.QuizSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.QuizSessions.UpdateAsync(vm.QuizSession);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizSessionExists(vm.QuizSession.Id))
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

            vm.Users = new SelectList(await _userManager.Users.ToListAsync(), nameof(AppUser.Id),
                nameof(AppUser.FullName));
            vm.Quizzes = new SelectList(await _uow.Quizzes.GetAllAsync(), nameof(Quiz.Id), nameof(Quiz.Title));
            return View(vm);
        }

        // GET: QuizSessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizSession = await _uow.QuizSessions
                .FirstOrDefaultAsync(id.Value);
            if (quizSession == null)
            {
                return NotFound();
            }

            return View(quizSession);
        }

        // POST: QuizSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizSession = await _uow.QuizSessions.FirstOrDefaultAsync(id);
            await _uow.QuizSessions.RemoveAsync(quizSession);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizSessionExists(Guid id)
        {
            return _uow.QuizSessions.Any(e => e.Id == id);
        }
    }
}