using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class OrderRow : DomainEntityBaseMetadata
    {
        [MaxLength(36)] public Guid ItemId { get; set; }
        public Item Item { get; set; } = default!;
        [MaxLength(36)] public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        [Range(1, 20)] public int Amount { get; set; } = default!;
        public Decimal Cost { get; set; } = default!;
        [MaxLength(512)] public string Comment { get; set; } = default!;
    }
}