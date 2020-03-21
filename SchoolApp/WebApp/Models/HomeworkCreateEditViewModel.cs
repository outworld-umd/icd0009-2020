using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models {

    public class HomeworkCreateEditViewModel {
        public Homework Homework { get; set; }
        public SelectList SubjectGroups { get; set; }
        public SelectList Teachers { get; set; }
    }

}