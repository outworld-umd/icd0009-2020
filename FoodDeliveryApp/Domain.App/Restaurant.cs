using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class Restaurant : Restaurant<Guid>, IDomainEntityIdMetadata
    {
    
    }

    public class Restaurant<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Phone { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string Address { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; }

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }
        public ICollection<RestaurantUser>? RestaurantUsers { get; set; }
    }
}