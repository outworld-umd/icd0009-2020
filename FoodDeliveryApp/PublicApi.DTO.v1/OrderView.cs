using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Domain.App.Enums;

namespace PublicApi.DTO.v1
{
    public class OrderView : IDomainEntityId
    {
        public OrderStatus OrderStatus { get; set; }
        public decimal FoodCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public string? RestaurantName { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Guid Id { get; set; } = default!;
    }
}