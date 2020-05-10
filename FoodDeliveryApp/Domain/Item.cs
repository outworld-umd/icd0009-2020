using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Item : DomainEntityBaseMetadata
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MaxLength(512)] public string? PictureLink { get; set; }
        [Column(TypeName = "decimal(6,2)")]public decimal Price { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; }
        


        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
    }
}