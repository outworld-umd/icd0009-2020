using System.Collections.Generic;
using Domain.Identity;

namespace WebApp.ViewModels
{
    public class UserManagerIndexViewModel
    {
        public List<AppUser> Users = default!;

        public Dictionary<AppUser, AppRole> UserRoles = default!;
    }
}