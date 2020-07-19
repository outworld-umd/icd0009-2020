using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Category : Category<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class Category<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>

    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        
    }
}