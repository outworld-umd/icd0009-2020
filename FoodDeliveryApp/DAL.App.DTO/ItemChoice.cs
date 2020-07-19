using System;
using System.Collections.Generic;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ItemChoice : ItemChoice<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class ItemChoice<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }
        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }


    }
}