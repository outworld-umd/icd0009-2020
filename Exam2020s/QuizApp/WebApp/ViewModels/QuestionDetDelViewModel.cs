using System.Collections.Generic;
using DAL.App.DTO;

namespace WebApp.ViewModels
{
    public class QuestionDetDelViewModel
    {
        public Question Question { get; set; } = default!;

        public Choice Choice { get; set; } = default!;
    }
}