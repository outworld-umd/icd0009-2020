using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class ItemOptionCreateEditViewModel {
        public ItemOption ItemOption { get; set; } = default!;
        public SelectList? Items { get; set; }
    }

}