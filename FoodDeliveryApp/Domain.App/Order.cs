using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using Domain.App.Enums;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Order : Order<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class Order<TKey, TUser>: DomainEntityIdMetadataUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        [Required] public OrderStatus OrderStatus { get; set; } = OrderStatus.Unfinished;
        [Required] public PaymentMethod PaymentMethod { get; set; }

        [Column(TypeName = "decimal(6,2)")] [Required] public decimal FoodCost { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal DeliveryCost { get; set; }
        [MinLength(2)] [MaxLength(512)] [Required] public string Address { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string? Apartment { get; set; }

        [MinLength(2)] [MaxLength(64)] public string? RestaurantName { get; set; }
        [MaxLength(64)] public string? Name { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }

        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }
    }
}