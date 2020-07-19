using System;
using System.Collections.Generic;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class ItemType : ItemType<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class ItemType<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }

    }
}