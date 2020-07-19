using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain;
using Domain.App.Enums;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Order : Order<Guid, AppUser>, IDomainBaseEntityMetadata
    {
    }

    public class Order<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
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
        public Restaurant? Restaurant { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }

        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }
}