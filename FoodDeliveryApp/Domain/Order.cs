using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Order : DomainEntityBaseMetadata
    {
        public OrderStatus OrderStatus { get; set; } = default!;
        public PaymentMethod PaymentMethod { get; set; } = default!;

        public Decimal FoodCost { get; set; } = default!;
        public Decimal DeliveryCost { get; set; } = default!;
        [MaxLength(512)] public string Comment { get; set; } = default!;

        
        [MaxLength(36)] public Guid? OrderAddressId { get; set; }
        public OrderAddress? OrderAddress { get; set; }


        [MaxLength(36)] public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        [MaxLength(36)] public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = default!;

        public ICollection<OrderRow>? OrderRows { get; set; }
    }
}