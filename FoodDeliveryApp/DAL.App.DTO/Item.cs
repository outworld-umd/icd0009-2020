using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace DAL.App.DTO.Identity
{
    public class Item : Item<Guid, AppUser>, IDomainBaseEntity
    {
        
    }
    
    public class Item<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
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