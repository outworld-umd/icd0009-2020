using System;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemInType : ItemInType<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemInType<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemTypeId { get; set; } = default!;
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}