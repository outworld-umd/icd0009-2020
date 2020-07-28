using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Restaurant : Restaurant<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }
    
    
    public class Restaurant<TKey, TUser> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
        
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] [Required] public string Phone { get; set; } = default!;
        [MinLength(2)] [MaxLength(512)] [Required] public string Address { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal DeliveryCost { get; set; }


        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }
        public ICollection<RestaurantUser>? RestaurantUsers { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}