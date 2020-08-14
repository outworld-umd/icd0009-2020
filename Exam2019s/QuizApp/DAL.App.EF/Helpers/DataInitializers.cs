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
            var roleNames = new[] {"Admin", "Customer"};
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }

            // ================ ADMIN TEST USER ================

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
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }

                var identityResult = userManager.AddToRoleAsync(user, "Admin").Result;
            }

            // ================ CUSTOMER TEST USER ================


            userName = "test@cust.com";
            passWord = "Test.cust.2020";
            firstName = "Test";
            lastName = "Customer";
            phone = "88005553535";

            user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser
                {
                    Email = userName,
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                };

                var result = userManager.CreateAsync(user, passWord).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }

                var identityResult = userManager.AddToRoleAsync(user, "Customer").Result;
            }
        }

        public static void SeedData(AppDbContext context)
        {
        //     var quizzes = new Quiz[]
        //     {
        //         new Quiz()
        //         {
        //             Id = new Guid("00000000-0000-0000-0000-000000000001")
        //         },
        //     };
        //     
        //     foreach (var quiz in quizzes)
        //     {
        //         if (!context.Quizzes.Any(l => l.Id == quiz.Id))
        //         {
        //             context.Quizzes.Add(quiz);
        //         }
        //     }
        //
        //     context.SaveChanges();
        }
    }
}