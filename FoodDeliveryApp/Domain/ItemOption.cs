﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class ItemOption : ItemOption<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class ItemOption<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }


        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public ICollection<ItemChoice>? ItemChoices { get; set; }
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }
}