using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Restaurant : Restaurant<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    
    public class Restaurant<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
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