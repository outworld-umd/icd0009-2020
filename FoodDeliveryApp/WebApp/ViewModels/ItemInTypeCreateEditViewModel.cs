using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class ItemInTypeCreateEditViewModel {
        public ItemInType ItemInType { get; set; } = default!;
        public SelectList? Items { get; set; }
        public SelectList? ItemTypes { get; set; }
    }

}