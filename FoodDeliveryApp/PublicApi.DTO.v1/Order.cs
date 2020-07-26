using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Domain.App.Enums;

namespace PublicApi.DTO.v1
{
    public class Order : OrderView
    {
        public PaymentMethod PaymentMethod { get; set; }

        public string Address { get; set; } = default!;
        public string? Apartment { get; set; }
        public string? Comment { get; set; }

        public Guid? RestaurantId { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }
        
    }
    
    public class OrderView
    {
        public OrderStatus OrderStatus { get; set; }
        public decimal FoodCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public string? RestaurantName { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Guid Id { get; set; } = default!;
    }

    public class OrderPatch 
    {
        public OrderStatus OrderStatus { get; set; }
    }
}