using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class OrderAddress : DomainEntityBaseMetadata
    {
        [MaxLength(36)] public Guid OrderId { get; set; } = default!;
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }


        [MaxLength(36)] public Guid AddressId { get; set; }
        public Address Address { get; set; } = default!;
    }
}