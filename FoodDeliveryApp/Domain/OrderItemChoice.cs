using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class OrderItemChoice : DomainEntityBaseMetadata
    {
        [Range(1, 20)] public int Amount { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] public decimal Cost { get; set; } = default!;

        public Guid? OrderRowId { get; set; }
        public OrderRow? OrderRow { get; set; } = default!;

        public Guid ItemChoiceId { get; set; }
        public ItemChoice? ItemChoice { get; set; } = default!;
    }
}