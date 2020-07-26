using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class Item : ItemView
    {
        public string? Description { get; set; }
        public Guid RestaurantId { get; set; }
        
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
    }
    public class ItemView
    {
        public string Name { get; set; } = default!;
        public string? PictureLink { get; set; }
        public decimal Price { get; set; }

        public Guid Id { get; set; } = default!;
    }
}