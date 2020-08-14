using System;
using DAL.App.DTO;
using Domain.App.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class AnswersCreateEditViewModel
    {
        public Answer Answer { get; set; } = default!;
        public SelectList? Choices { get; set; }
        public SelectList? QuizSessions { get; set; }
        
    }
}