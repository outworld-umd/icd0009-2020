using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class ItemChoiceCreateEditViewModel {
        public ItemChoice ItemChoice { get; set; } = default!;
        public SelectList? ItemOptions { get; set; }
    }

}