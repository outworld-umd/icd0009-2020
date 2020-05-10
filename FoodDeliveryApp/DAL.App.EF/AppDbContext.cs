using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppDbContext : IdentityDbContext
    {

        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemChoice> ItemChoices { get; set; } = default!;
        public DbSet<ItemOption> ItemOptions { get; set; } = default!;
        public DbSet<ItemType> ItemTypes { get; set; } = default!;
        public DbSet<NutritionInfo> NutritionInfos { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItemChoice> OrderItemChoices { get; set; } = default!;
        public DbSet<OrderRow> OrderRows { get; set; } = default!;
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; } = default!;
        public DbSet<WorkingHours> WorkingHourses { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
    }

}