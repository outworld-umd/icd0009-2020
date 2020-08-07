using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class ItemTypeCreateEditViewModel {
        public ItemType ItemType { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}