using System.Collections.Generic;
using Domain.App.Identity;

namespace WebApp.Areas.Admin.ViewModels
{
    public class UserManagerIndexViewModel
    {
        public List<AppUser> Users = default!;

        public Dictionary<AppUser, AppRole> UserRoles = default!;
    }
}