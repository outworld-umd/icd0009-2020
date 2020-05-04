using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class ItemChoice : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public Decimal AdditionalPrice { get; set; } = default!;

        [MaxLength(36)] public Guid ItemOptionId { get; set; }
        public ItemOption ItemOption { get; set; } = default!;
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
    }
}