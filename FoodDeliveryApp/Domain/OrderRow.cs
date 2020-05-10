using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OrderRow : OrderRow<Guid, AppUser>, IDomainEntityBaseMetadata {
        
    }
    
    public class OrderRow<TKey, TUser> : DomainEntityBaseMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        [Range(1, 20)] public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] public decimal Cost { get; set; }
        [MaxLength(512)] public string? Comment { get; set; }
        
    }
}