using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Restaurant : Restaurant<Guid, AppUser>, IDomainBaseEntity
    {
        
    }
    
    
    public class Restaurant<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }
        public ICollection<RestaurantUser>? RestaurantUsers { get; set; }

    }
}