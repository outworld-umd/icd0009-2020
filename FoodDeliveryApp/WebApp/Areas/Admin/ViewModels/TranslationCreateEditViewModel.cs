using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class TranslationCreateEditViewModel
    {
        public Translation Translation { get; set; } = default!;
        public SelectList? LangStringIds { get; set; }
    }
}