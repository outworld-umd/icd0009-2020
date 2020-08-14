using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ChoicesCreateEditViewModel
    {
        public Choice Choice { get; set; } = default!;

        public SelectList? Questions { get; set; }
    }
}