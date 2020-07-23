using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Domain.App.Enums;

namespace PublicApi.DTO.v1
{
    public class Order : Order<Guid>, IDomainEntityId
    {
    }

    public class Order<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public decimal FoodCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Address { get; set; } = default!;
        public string? Apartment { get; set; }

        public string? RestaurantName { get; set; }
        public string? Comment { get; set; }

        public Guid? RestaurantId { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public TKey Id { get; set; } = default!;
    }
}