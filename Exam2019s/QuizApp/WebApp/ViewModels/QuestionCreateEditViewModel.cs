using System;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class QuestionCreateEditViewModel
    {
        public Question Question { get; set; } = default!;

        public SelectList? CorrectChoices { get; set; }
        public SelectList? Quizzes { get; set; }
    }
}