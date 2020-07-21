using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain.Combined;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class OrderRow<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        [Range(1, 20)] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal Cost { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }
    }
}