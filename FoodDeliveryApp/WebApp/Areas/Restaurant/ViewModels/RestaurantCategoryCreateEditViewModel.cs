using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class RestaurantCategoryCreateEditViewModel {
        public RestaurantCategory RestaurantCategory { get; set; } = default!;
        public SelectList? Categories { get; set; }
        public SelectList? Restaurants { get; set; }
    }

}