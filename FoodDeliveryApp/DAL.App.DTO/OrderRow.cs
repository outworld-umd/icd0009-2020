using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class OrderRow : OrderRow<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class OrderRow<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }

    }
}