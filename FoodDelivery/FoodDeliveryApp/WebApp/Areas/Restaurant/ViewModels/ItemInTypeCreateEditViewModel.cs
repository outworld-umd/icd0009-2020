using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class ItemInTypeCreateEditViewModel {
        public ItemInType ItemInType { get; set; } = default!;
        public SelectList? Items { get; set; }
        public SelectList? ItemTypes { get; set; }
    }

}