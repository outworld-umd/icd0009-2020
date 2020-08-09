using System;
using System.Collections;
using System.Collections.Generic;
using Contracts.Domain.Base.Basic;
namespace PublicApi.DTO.v1
{
    public class Restaurant : RestaurantView
    {
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<WorkingHours>? WorkingHourses { get; set; }
        public ICollection<ItemType>? ItemTypes { get; set; }

    }
    
    public class RestaurantView
    {
        public string Name { get; set; } = default!;
        public decimal DeliveryCost { get; set; }

        public IEnumerable<CategoryView>? Categories { get; set; }
        public bool IsOpen { get; set; }

        public Guid Id { get; set; } = default!;
    }
}