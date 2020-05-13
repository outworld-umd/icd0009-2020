using Domain.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class UserManagerModel
    {
        public AppUser User { get; set; } = default!;

        public SelectList? Roles { get; set; }
    }
}