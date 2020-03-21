using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models {

    public class PersonFormCreateEditViewModel {
        public PersonForm PersonForm { get; set; }
        public SelectList Forms { get; set; }
        public SelectList FormRoles { get; set; }
        public SelectList Persons { get; set; }
    }

}