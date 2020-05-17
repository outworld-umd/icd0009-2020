using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ItemChoice : ItemChoice<Guid, AppUser>, IDomainBaseEntity<Guid> {
        
    }
    
    public class ItemChoice<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }
        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }


    }
}