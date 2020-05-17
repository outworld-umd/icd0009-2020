using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;

namespace BLL.App.DTO
{
    public class Order : Order<Guid, AppUser>, IDomainBaseEntity
    {
    }

    public class Order<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
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