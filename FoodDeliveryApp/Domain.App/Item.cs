﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using DAL.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    
    public class Item : Item<Guid, AppUser>, IDomainBaseEntityMetadata {
   
    }
    public class Item<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [MaxLength(512)] public string? PictureLink { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal Price { get; set; }
        [MaxLength(512)] public string? Description { get; set; }
        


        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
        
    }
}