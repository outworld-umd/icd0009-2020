using System;
using System.Collections.Generic;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
namespace DAL.App.DTO
{
    public class ItemType : ItemType<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class ItemType<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public ICollection<ItemInType>? ItemInTypes { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}