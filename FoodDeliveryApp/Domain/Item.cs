using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Item : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MaxLength(512)] public string PictureLink { get; set; } = default!;
        public Decimal Price { get; set; } = default!;
        [MaxLength(512)] public string Description { get; set; } = default!;
        
        [MaxLength(36)] public Guid ItemTypeId { get; set; }
        public ItemType ItemType { get; set; } = default!;

        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
    }
}