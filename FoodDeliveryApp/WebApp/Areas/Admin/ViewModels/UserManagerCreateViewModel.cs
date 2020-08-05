using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserManagerCreateViewModel
    {
        public AppUser User { get; set; } = default!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = default!;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = default!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = default!;

        [Display(Name = "Role")]
        public SelectList? Roles { get; set; }

        public AppRole? Role { get; set; }
    }
}