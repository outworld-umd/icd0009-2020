using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class ItemChoiceCreateEditViewModel {
        public ItemChoice ItemChoice { get; set; } = default!;
        public SelectList? ItemOptions { get; set; }
    }

}