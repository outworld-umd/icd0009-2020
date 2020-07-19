using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class ItemOption : ItemOption<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class ItemOption<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }


        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public ICollection<ItemChoice>? ItemChoices { get; set; }
        
    }
}