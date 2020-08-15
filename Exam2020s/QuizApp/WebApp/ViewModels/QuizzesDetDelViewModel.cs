using System;
using DAL.App.DTO;

namespace WebApp.ViewModels
{
    public class QuizzesDetDelViewModel
    {
        public Question Question { get; set; } = default!;
        public Quiz Quiz { get; set; } = default!;
    }
}