using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Category : Category<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class Category<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>

    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }
}