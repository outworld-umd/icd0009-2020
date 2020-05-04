using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class OrderItemChoice : DomainEntityBaseMetadata
    {
        [Range(1, 20)] public int Amount { get; set; } = default!;
        public Decimal Cost { get; set; } = default!;

        [MaxLength(36)] public Guid OrderRowId { get; set; }
        public OrderRow OrderRow { get; set; } = default!;

        [MaxLength(36)] public Guid ItemChoiceId { get; set; }
        public ItemChoice ItemChoice { get; set; } = default!;
    }
}