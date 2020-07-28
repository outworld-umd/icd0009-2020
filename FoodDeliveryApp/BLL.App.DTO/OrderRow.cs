using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Combined;

namespace BLL.App.DTO
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class OrderRow<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid? ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string Name { get; set; } = default!;

        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        [Range(1, 20)] [Required] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal Cost { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}