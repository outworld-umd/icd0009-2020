using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class NutritionInfo : DomainEntityBaseMetadata
    {
        [MaxLength(36)] public Guid ItemId { get; set; }
        public Item Item { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public Decimal Amount { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Unit { get; set; } = default!;
    }
}