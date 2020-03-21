using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models {

    public class GradeColumnCreateEditViewModel {
        public GradeColumn Column { get; set; }
        public SelectList Groups { get; set; }
    }

}