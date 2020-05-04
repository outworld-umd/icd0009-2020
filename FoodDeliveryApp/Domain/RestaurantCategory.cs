using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class RestaurantCategory : DomainEntityBaseMetadata
    {
        [MaxLength(36)] public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        [MaxLength(36)] public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = default!;
    }
}