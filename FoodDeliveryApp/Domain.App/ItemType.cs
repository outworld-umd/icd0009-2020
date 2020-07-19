using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class ItemType : ItemType<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class ItemType<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        [MaxLength(512)] public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        
    }
}