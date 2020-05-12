using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class OrderCreateEditViewModel {
        public Order Order { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}