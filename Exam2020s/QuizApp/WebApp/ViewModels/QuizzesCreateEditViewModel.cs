using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class QuizzesCreateEditViewModel
    {
        public Quiz Quiz { get; set; } = default!;
    }
}