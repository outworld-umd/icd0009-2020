using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models {

    public class GradeCreateEditViewModel {
        public Grade Grade { get; set; }
        public SelectList AbsenceReasons { get; set; }
        public SelectList StudentGroups { get; set; }
        public SelectList Teachers { get; set; }
        public SelectList GradeColumns { get; set; }
    }

}