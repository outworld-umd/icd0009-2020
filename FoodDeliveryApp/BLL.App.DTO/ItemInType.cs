using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemInType : ItemInType<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class ItemInType<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey ItemTypeId { get; set; } = default!;
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
}