﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Combined;
using Domain.Base;

namespace Domain.App
{
    public class ItemChoice : ItemChoice<Guid>, IDomainEntityIdMetadata
    {
    }

    public class ItemChoice<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] public decimal AdditionalPrice { get; set; }

        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
    }
}