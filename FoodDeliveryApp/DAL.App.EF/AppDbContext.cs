using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemChoice> ItemChoices { get; set; } = default!;
        public DbSet<ItemInType> ItemInTypes { get; set; } = default!;
        public DbSet<ItemOption> ItemOptions { get; set; } = default!;
        public DbSet<ItemType> ItemTypes { get; set; } = default!;
        public DbSet<NutritionInfo> NutritionInfos { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItemChoice> OrderItemChoices { get; set; } = default!;
        public DbSet<OrderRow> OrderRows { get; set; } = default!;
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<RestaurantUser> RestaurantUsers { get; set; } = default!;
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; } = default!;
        public DbSet<WorkingHours> WorkingHourses { get; set; } = default!;
        
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Restaurant)
                .WithMany(b => b!.Orders)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}