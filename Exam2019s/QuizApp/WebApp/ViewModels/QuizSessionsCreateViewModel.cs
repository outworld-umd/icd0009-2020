using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class QuizSessionsCreateViewModel
    {
        public QuizSession QuizSession { get; set; } = default!;

        public SelectList? Users { get; set; }
        public SelectList? Quizzes { get; set; }
    }
}