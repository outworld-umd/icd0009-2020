using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var roleNames = new string[] {"Customer", "Admin", "Restaurant"};
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


            var userName = "dubistdum@dubistdum.com";
            var passWord = "Kala.maja.2020";
            var firstName = "DuBistDumm";
            var lastName = "DuBistDumm";
            var phone = "3453245632";

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
            }


            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "Customer").Result;
        }

        public static void SeedData(AppDbContext context)
        {
            // insert predefined location types
            var categories = new Category[]
            {
                new Category()
                {
                    Name =  "Sushi",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Category()
                {
                    Name =  "Burger",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Category()
                {
                    Name =  "Pasta",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new Category()
                {
                    Name =  "Pizza",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                },
                new Category()
                {
                    Name =  "Vegan",
                    Id = new Guid("00000000-0000-0000-0000-000000000005")
                },
                new Category()
                {
                    Name =  "Healthy",
                    Id = new Guid("00000000-0000-0000-0000-000000000006")
                },
                new Category()
                {
                    Name =  "Dessert",
                    Id = new Guid("00000000-0000-0000-0000-000000000007")
                },
                new Category()
                {
                    Name =  "Noodles",
                    Id = new Guid("00000000-0000-0000-0000-000000000008")
                },
                new Category()
                {
                    Name =  "Breakfast",
                    Id = new Guid("00000000-0000-0000-0000-000000000009")
                },
                new Category()
                {
                    Name =  "Smoothie",
                    Id = new Guid("00000000-0000-0000-0000-000000000010")
                },
                new Category()
                {
                    Name =  "Kebab",
                    Id = new Guid("00000000-0000-0000-0000-000000000011")
                },
                new Category()
                {
                    Name =  "Salad",
                    Id = new Guid("00000000-0000-0000-0000-000000000012")
                },
                new Category()
                {
                    Name =  "Vegetarian",
                    Id = new Guid("00000000-0000-0000-0000-000000000013")
                },
                new Category()
                {
                    Name =  "Ramen",
                    Id = new Guid("00000000-0000-0000-0000-000000000014")
                },
                new Category()
                {
                    Name =  "Smoothie",
                    Id = new Guid("00000000-0000-0000-0000-000000000015")
                },
                new Category()
                {
                    Name =  "Kebab",
                    Id = new Guid("00000000-0000-0000-0000-000000000016")
                },
                new Category()
                {
                    Name =  "Salad",
                    Id = new Guid("00000000-0000-0000-0000-000000000017")
                },
                new Category()
                {
                    Name =  "Mexican",
                    Id = new Guid("00000000-0000-0000-0000-000000000018")
                },
                new Category()
                {
                    Name =  "Thai",
                    Id = new Guid("00000000-0000-0000-0000-000000000019")
                },
                new Category()
                {
                    Name =  "Italian",
                    Id = new Guid("00000000-0000-0000-0000-000000000020")
                },
                new Category()
                {
                    Name =  "Indian",
                    Id = new Guid("00000000-0000-0000-0000-000000000021")
                },
                new Category()
                {
                    Name =  "Japanese",
                    Id = new Guid("00000000-0000-0000-0000-000000000022")
                },
                new Category()
                {
                    Name =  "American",
                    Id = new Guid("00000000-0000-0000-0000-000000000023")
                },
                new Category()
                {
                    Name =  "Chinese",
                    Id = new Guid("00000000-0000-0000-0000-000000000024")
                },
                new Category()
                {
                    Name =  "Vietnamese",
                    Id = new Guid("00000000-0000-0000-0000-000000000025")
                },
                new Category()
                {
                    Name =  "Nepalese",
                    Id = new Guid("00000000-0000-0000-0000-000000000026")
                },
                new Category()
                {
                    Name =  "Georgian",
                    Id = new Guid("00000000-0000-0000-0000-000000000027")
                },
                new Category()
                {
                    Name =  "Street Food",
                    Id = new Guid("00000000-0000-0000-0000-000000000028")
                },
                new Category()
                {
                    Name =  "Steak",
                    Id = new Guid("00000000-0000-0000-0000-000000000029")
                },
                new Category()
                {
                    Name =  "Mediterranean",
                    Id = new Guid("00000000-0000-0000-0000-000000000030")
                },
                new Category()
                {
                    Name =  "Sandwich",
                    Id = new Guid("00000000-0000-0000-0000-000000000031")
                },
                new Category()
                {
                    Name =  "Soup",
                    Id = new Guid("00000000-0000-0000-0000-000000000032")
                },
                new Category()
                {
                    Name =  "Fish",
                    Id = new Guid("00000000-0000-0000-0000-000000000033")
                },
                new Category()
                {
                    Name =  "Cafe",
                    Id = new Guid("00000000-0000-0000-0000-000000000034")
                },
                new Category()
                {
                    Name =  "Tapas",
                    Id = new Guid("00000000-0000-0000-0000-000000000035")
                }
                
            };

            foreach (var category in categories)
            {
                if (!context.Categories.Any(l => l.Id == category.Id))
                {
                    context.Categories.Add(category);
                }
            }

            context.SaveChanges();

            var restaurants = new Restaurant[]
            {
                new Restaurant()
                {
                    Name = "KFC Kristiine",
                    Phone  = "550 1234",
                    Address = "Endla 45",
                    DeliveryCost = 3
                },
                new Restaurant()
                {
                    Name = "Burger King Rocca al Mare",
                    Phone  = "665 9345",
                    Address = "Paldiski maantee 102",
                    DeliveryCost = 3
                },
                new Restaurant()
                {
                    Name = "McDonald's Mustamäe",
                    Phone  = "5561 7012",
                    Address = " A. H. Tammsaare tee 76",
                    DeliveryCost = 3
                },
                new Restaurant()
                {
                    Name = "Dodo Pizza Sõpruse pst.",
                    Phone  = "629 9209",
                    Address = "Sõpruse pst. 211a",
                    DeliveryCost = 3
                },
            };
            
            foreach (var restaurant in restaurants)
            {
                if (!context.Restaurants.Any(l => l.Id == restaurant.Id))
                {
                    context.Restaurants.Add(restaurant);
                }
            }
            
            var addresses = new Address[]
            {
                new Address()
                {
                    County = "Harjumaa",
                    City = "Tallinn",
                    Street = "Akadeemia tee",
                    BuildingNumber = "7/2",
                    Apartment = "201b",
                    Name = "Uhikas",
                    AppUserId = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Address()
                {
                    County = "Harjumaa",
                    City = "Tallinn",
                    Street = "Sõpruse pst.",
                    BuildingNumber = "212",
                    Apartment = "27",
                    Name = "Home",
                    AppUserId = new Guid("00000000-0000-0000-0000-000000000001")
                },
            };
            
            foreach (var address in addresses)
            {
                if (!context.Addresses.Any(l => l.Id == address.Id))
                {
                    context.Addresses.Add(address);
                }
            }

            context.SaveChanges();
        }
    }
}