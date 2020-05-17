using System;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class OrderItemChoice : OrderItemChoice<Guid, AppUser>, IDomainBaseEntity
    {
        
    }
    
    
    public class OrderItemChoice<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public TKey OrderRowId { get; set; } = default!; // ZDES BIL 0!!!!!!!!!!!!!!!
        public OrderRow? OrderRow { get; set; }

        public TKey ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }

    }
}