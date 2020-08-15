using System;
using System.Collections.Generic;
using DAL.App.DTO;

namespace WebApp.ViewModels
{
    public class TakeQuizViewModel
    {
        public Quiz Quiz { get; set; } = default!;
        public List<Guid>? ChosenIds { get; set; }
    }
}