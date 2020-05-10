using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OrderItemChoice : OrderItemChoice<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class OrderItemChoice<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey>
    {
        [Range(1, 20)] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal Cost { get; set; }

        public TKey OrderRowId { get; set; } = default!; // ZDES BIL 0!!!!!!!!!!!!!!!
        public OrderRow? OrderRow { get; set; }

        public TKey ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }
        
    }
}