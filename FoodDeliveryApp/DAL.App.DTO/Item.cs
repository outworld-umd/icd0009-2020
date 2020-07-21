using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Item : Item<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class Item<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
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