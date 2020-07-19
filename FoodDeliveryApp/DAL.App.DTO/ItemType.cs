using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemType : ItemType<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class ItemType<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }

    }
}