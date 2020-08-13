using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public QuizzesController(IAppUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Quizzes.GetAllAsync());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes
                .FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_userManager.Users.ToList(), "Id", "FirstName");
            return View();
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Description,QuizType,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quiz.Id = Guid.NewGuid();
                _uow.Quizzes.Add(quiz);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AppUserId"] = new SelectList(_userManager.Users.ToList(), "Id", "FirstName");
            return View(quiz);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            ViewData["AppUserId"] = new SelectList(_userManager.Users.ToList(), "Id", "FirstName");
            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Title,Description,QuizType,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.Quizzes.UpdateAsync(quiz);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
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

            ViewData["AppUserId"] = new SelectList(_userManager.Users.ToList(), "Id", "FirstName");
            return View(quiz);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quizzes
                .FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);
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