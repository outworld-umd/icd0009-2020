using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class ItemOptionCreateEditViewModel {
        public ItemOption ItemOption { get; set; } = default!;
        public SelectList? Items { get; set; }
    }

}