using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    
    public class ItemChoice : ItemChoice<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    public class ItemChoice<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] public decimal AdditionalPrice { get; set; }

        public TKey ItemOptionId { get; set; } = default!;
        public ItemOption? ItemOption { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
    }
}