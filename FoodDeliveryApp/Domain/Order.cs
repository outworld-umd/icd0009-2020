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

        [Column(TypeName = "decimal(6,2)")] public decimal FoodCost { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] public decimal DeliveryCost { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string Address { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string? Apartment { get; set; }
        
        [MinLength(2)] [MaxLength(64)] public string? RestaurantName { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }


        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; } = default!;

        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; } = default!;

        public ICollection<OrderRow>? OrderRows { get; set; }
    }
}