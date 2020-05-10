using System;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class ItemInType : ItemInType<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    public class ItemInType<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        public TKey ItemTypeId { get; set; } = default!;
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
    }
}