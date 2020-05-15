using System.ComponentModel.DataAnnotations;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserManagerDetailsDeleteViewModel
    {
        public AppUser User { get; set; } = default!;

        public AppRole Role { get; set; }
    }
}