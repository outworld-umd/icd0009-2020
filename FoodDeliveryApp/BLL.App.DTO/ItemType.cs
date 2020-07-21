using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemType : ItemType<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class ItemType<TKey> : DomainEntityIdMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }

    }
}