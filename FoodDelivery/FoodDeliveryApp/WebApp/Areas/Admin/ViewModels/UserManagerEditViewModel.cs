using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Domain.Identity;using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class UserManagerEditViewModel
    {
        public AppUser User { get; set; } = default!;
        

        [Display(Name = "Role")]
        public SelectList? Roles { get; set; } = default!;

        public AppRole? Role { get; set; } = default!;

    }
}