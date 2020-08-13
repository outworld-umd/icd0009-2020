using System;
using System.Linq;
using Domain.App;
using ee.itcollege.anguzo.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppUser = Domain.App.Identity.AppUser;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var userName = "test@admi.com";
            var passWord = "Test.admi.2020";
            var firstName = "Test";
            var lastName = "Admin";
            var phone = "88005553535";
            
            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser
                {
                    Email = userName,
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                };

                var result = userManager.CreateAsync(user, passWord).Result;
            }
        }

        public static void SeedData(AppDbContext context)
        {
        }
    }
}