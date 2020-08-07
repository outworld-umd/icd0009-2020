using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace WebApp.Areas.Admin.ViewModels
{
    public class UserManagerDetailsDeleteViewModel
    {
        public AppUser User { get; set; } = default!;
        public AppRole Role { get; set; } = default!;
        
        [Display(Name = "Current Roles")]
        public string CurrentRoles { get; set; } = default!;
    }
}