using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;
namespace Domain.App
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class OrderRow<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public Guid? ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string? Name { get; set; } = default!;

        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        [Range(1, 20)] [Required] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal Cost { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
    }
}