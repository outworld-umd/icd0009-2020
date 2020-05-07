using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class RestaurantCategory : DomainEntityBaseMetadata
    {
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; } = default!;

        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; } = default!;
    }
}