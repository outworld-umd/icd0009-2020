using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserManagerDetailsDeleteViewModel
    {
        public AppUser User { get; set; } = default!;
        public AppRole Role { get; set; }
        
        [Display(Name = "Current Roles")]
        public string CurrentRoles { get; set; }
    }
}