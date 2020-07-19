using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private IUserNameProvider _userNameProvider;

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
        
        

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider) : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Restaurant)
                .WithMany(b => b.Orders)
                .OnDelete(DeleteBehavior.SetNull);
        }
        
        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();
            
            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
            
        }
        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            return base.SaveChangesAsync(cancellationToken);
        }
        
    }

}