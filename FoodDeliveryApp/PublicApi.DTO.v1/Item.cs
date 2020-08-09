using System;
using System.Collections.Generic;
using Contracts.Domain.Base.Basic;
namespace PublicApi.DTO.v1
{
    public class Item
    {
        public string? Description { get; set; }
        public Guid RestaurantId { get; set; }
        
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? Options { get; set; }
        public string Name { get; set; } = default!;
        public string? PictureLink { get; set; }
        public decimal Price { get; set; }

        public Guid Id { get; set; }
    }
    
    public class ItemView
    {
        public Guid ItemInTypeId { get; set; }
        public string Name { get; set; } = default!;
        public string? PictureLink { get; set; }
        public decimal Price { get; set; }

        public Guid Id { get; set; }
    }
}