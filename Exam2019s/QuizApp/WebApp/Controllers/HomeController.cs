using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, IAppUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public IActionResult Index()
        {
            var vm = new QuizViewModel
            {
                Quizzes = _uow.Quizzes.GetAll().Where(q => q.Questions!.Count > 0).ToList(),
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        // Get: TakeQuiz
        public async Task<IActionResult> TakeQuiz(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var vmend = new TakeQuizViewModel
            {
                Quiz = await _uow.Quizzes.FirstOrDefaultAsync(id.Value)
            };
            return View(vmend);
        }
        
        // Post: TakeQuiz
        [HttpPost]
        public async Task<IActionResult> TakeQuiz(Guid id, TakeQuizViewModel vm)
        {
            Console.WriteLine(id);
            var quizSession = new QuizSession
            {
                QuizId = id,
                Answers = vm.ChosenIds.Select(e => new Answer
                {
                    ChoiceId = e,
                    IsCorrect = e.Equals((_uow.Choices.FirstOrDefaultAsync(e)).Result.Question!.CorrectChoiceId)
                }).ToList(),
                AppUserId = User.UserGuidId()
            };
            _uow.QuizSessions.Add(quizSession);
            await _uow.SaveChangesAsync();
            Console.WriteLine(JsonConvert.SerializeObject(quizSession));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );
            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}