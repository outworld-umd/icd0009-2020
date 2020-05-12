using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class ItemTypeCreateEditViewModel {
        public ItemType ItemType { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}