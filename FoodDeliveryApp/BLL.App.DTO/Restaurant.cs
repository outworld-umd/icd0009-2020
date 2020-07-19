using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Restaurant : Restaurant<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    
    public class Restaurant<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
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