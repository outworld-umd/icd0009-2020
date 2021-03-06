﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;

namespace Domain.App
{
    public class Restaurant : Restaurant<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Restaurant<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string Name { get; set; } = default!;

        [MinLength(2)]
        [MaxLength(64)]
        [Required]
        public string Phone { get; set; } = default!;

        [MinLength(2)]
        [MaxLength(512)]
        [Required]
        public string Address { get; set; } = default!;

        [MaxLength(512)] public string? Description { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        [Required]
        public decimal DeliveryCost { get; set; }


        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }
        public ICollection<RestaurantUser>? RestaurantUsers { get; set; }
    }
}