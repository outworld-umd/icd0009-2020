using System;
using Contracts.Domain.Combined;

namespace DAL.App.DTO
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class OrderRow<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}