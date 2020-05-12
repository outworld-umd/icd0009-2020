using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Restaurant : Restaurant<Guid, AppUser>, IDomainEntityBaseMetadata
    {
        //, IDomainEntityUser<AppUser>
    }

    // public class Restaurant<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
    //                                where TKey : IEquatable<TKey> 
    //                                where TUser : AppUser<TKey>

    public class Restaurant<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Phone { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string Address { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; }

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }

        // public TKey AppUserId { get; set; } = default!;
        // public TUser? AppUser { get; set; }
    }
}