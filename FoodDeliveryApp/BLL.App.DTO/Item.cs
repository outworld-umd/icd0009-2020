using System;
using System.Collections.Generic;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Item : Item<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class Item<TKey> : DomainEntityIdMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public string? PictureLink { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        
        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
    }
}