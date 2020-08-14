using System;
using System.Collections.Generic;
using DAL.App.DTO;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class QuizViewModel
    { 
        public ICollection<Quiz>? Quizzes { get; set; }
    }
}