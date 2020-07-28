using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        [Required] public OrderStatus OrderStatus { get; set; }
        [Required] public PaymentMethod PaymentMethod { get; set; }

        [Column(TypeName = "decimal(6,2)")] [Required] public decimal FoodCost { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal DeliveryCost { get; set; }
        [MinLength(2)] [MaxLength(512)] [Required] public string Address { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] public string? Apartment { get; set; }

        [MinLength(2)] [MaxLength(64)] public string? RestaurantName { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }

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