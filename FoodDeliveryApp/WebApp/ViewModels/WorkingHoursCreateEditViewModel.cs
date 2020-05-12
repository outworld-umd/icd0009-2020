using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class WorkingHoursCreateEditViewModel {
        public WorkingHours WorkingHours { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }

}