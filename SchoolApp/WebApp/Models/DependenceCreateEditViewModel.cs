using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models {

    public class DependenceCreateEditViewModel {
        public Dependence Dependence { get; set; }
        public SelectList Children { get; set; }
        public SelectList Parents { get; set; }
        public SelectList Types { get; set; }
    }

}