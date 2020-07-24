using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.App.Enums;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Order : Order<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Order<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public decimal FoodCost { get; set; }
        public decimal DeliveryCost { get; set; }
        public string Address { get; set; } = default!;
        public string? Apartment { get; set; }

        public string? RestaurantName { get; set; }
        public string? Comment { get; set; }

        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}