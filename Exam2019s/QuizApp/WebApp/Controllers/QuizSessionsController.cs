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
            ViewData["AppUserId"] = new SelectList(_userManager.Users.ToList(), "Id", "FirstName");
            ViewData["QuizId"] = new SelectList(_uow.Quizzes.GetAll(), "Id", "Title");
            return View();
        }

        // POST: QuizSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            QuizSession quizSession)
        {
            if (ModelState.IsValid)
            {
                quizSession.Id = Guid.NewGuid();
                _uow.QuizSessions.Add(quizSession);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AppUserId"] = new SelectList((await _userManager.Users.ToListAsync()), "Id",
                "FirstName");
            ViewData["QuizId"] = new SelectList((await _uow.Quizzes.GetAllAsync()), "Id", "Title");
            return View(quizSession);
        }

        // GET: QuizSessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizSession = await _uow.QuizSessions.FirstOrDefaultAsync(id.Value);
            if (quizSession == null)
            {
                return NotFound();
            }

            ViewData["AppUserId"] = new SelectList((await _userManager.Users.ToListAsync()), "Id",
                "FirstName");
            ViewData["QuizId"] = new SelectList((await _uow.Quizzes.GetAllAsync()), "Id", "Title");
            return View(quizSession);
        }

        // POST: QuizSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("QuizId,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")]
            QuizSession quizSession)
        {
            if (id != quizSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _uow.QuizSessions.UpdateAsync(quizSession);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizSessionExists(quizSession.Id))
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

            ViewData["AppUserId"] = new SelectList((await _userManager.Users.ToListAsync()), "Id",
                "FirstName");
            ViewData["QuizId"] = new SelectList((await _uow.Quizzes.GetAllAsync()), "Id", "Title");
            return View(quizSession);
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