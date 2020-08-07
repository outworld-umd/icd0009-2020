using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class NutritionInfoCreateEditViewModel {
        public NutritionInfo NutritionInfo { get; set; } = default!;
        public SelectList? Items { get; set; }
    }

}