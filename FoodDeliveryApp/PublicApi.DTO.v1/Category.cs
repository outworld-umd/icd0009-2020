using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using PublicApi.DTO.v1;

namespace PublicApi.DTO.v1
{
    public class Category
    {
        public string Name { get; set; } = default!;
        public ICollection<RestaurantView>? Restaurants { get; set; }

        public Guid Id { get; set; } = default!;
    }
    
    public class CategoryView
    {
        public string Name { get; set; } = default!;
        public Guid RestaurantCategoryId { get; set; }

        public Guid Id { get; set; } = default!;
    }
}