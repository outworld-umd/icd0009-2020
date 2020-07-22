using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityId
    {
    }
    
    public class OrderRow<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemId { get; set; } = default!;
        public string? ItemName { get; set; }
        public TKey OrderId { get; set; } = default!;
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
        public TKey Id { get; set; } = default!;
    }
}