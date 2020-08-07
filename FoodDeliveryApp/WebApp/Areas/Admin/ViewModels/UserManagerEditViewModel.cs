using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserManagerEditViewModel
    {
        public AppUser User { get; set; } = default!;
        

        [Display(Name = "Role")]
        public SelectList? Roles { get; set; } = default!;

        public AppRole? Role { get; set; } = default!;

        [Display(Name = "Current Roles")] public string CurrentRoles { get; set; } = default!;
    }
}