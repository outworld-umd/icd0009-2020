using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Restaurant : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MinLength(2)] [MaxLength(64)] public string Phone { get; set; } = default!;
        [MaxLength(512)] public string Address { get; set; } = default!;
        [MaxLength(512)] public string Description { get; set; } = default!;

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
    }
}