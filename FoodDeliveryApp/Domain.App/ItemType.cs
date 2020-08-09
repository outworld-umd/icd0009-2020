using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base.Combined;
using Domain.Base;

namespace Domain.App
{
    public class ItemType : ItemType<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemType<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey> 
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Required] public bool IsSpecial { get; set; }
        [MaxLength(512)] public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }
    }
}