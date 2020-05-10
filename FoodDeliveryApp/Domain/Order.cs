using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Order : Order<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class Order<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Column(TypeName = "decimal(6,2)")] public decimal FoodCost { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal DeliveryCost { get; set; }
        [MinLength(2)] [MaxLength(512)] public string Address { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string? Apartment { get; set; }
        
        [MinLength(2)] [MaxLength(64)] public string? RestaurantName { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }

        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public ICollection<OrderRow>? OrderRows { get; set; }
        
    }
}