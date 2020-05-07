using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class ItemType : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; }

        [MaxLength(36)] public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; } = default!;
    }
}