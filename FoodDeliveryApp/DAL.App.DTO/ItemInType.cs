using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemInType : ItemInType<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class ItemInType<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey ItemTypeId { get; set; } = default!;
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
}