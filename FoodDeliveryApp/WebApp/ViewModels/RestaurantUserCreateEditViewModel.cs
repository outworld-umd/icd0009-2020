using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class RestaurantUserCreateEditViewModel {
        public RestaurantUser RestaurantUser { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
        public SelectList? Users { get; set; }
    }

}