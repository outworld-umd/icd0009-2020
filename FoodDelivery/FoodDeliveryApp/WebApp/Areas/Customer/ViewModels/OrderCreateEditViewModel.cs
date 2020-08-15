using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Customer.ViewModels {

    public class OrderCreateEditViewModel {
        public Order Order { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}