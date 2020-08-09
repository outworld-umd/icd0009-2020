using System;
using System.Collections.Generic;
using Contracts.Domain.Base.Combined;

namespace DAL.App.DTO
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class OrderRow<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {

        public Guid? ItemId { get; set; } = default!;
        public Item? Item { get; set; } = default!;
        public string Name { get; set; } = default!;

        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}