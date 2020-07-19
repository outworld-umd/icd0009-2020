using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class OrderRow : OrderRow<Guid>, IDomainBaseEntity
    {
        
    }
    
    public class OrderRow<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }

    }
}