using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels {

    public class WorkingHoursCreateEditViewModel {
        public WorkingHours WorkingHours { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}