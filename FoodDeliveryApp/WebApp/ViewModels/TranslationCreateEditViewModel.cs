using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class TranslationCreateEditViewModel
    {
        public Translation Translation { get; set; } = default!;
        public SelectList? Translations { get; set; }
    }
}