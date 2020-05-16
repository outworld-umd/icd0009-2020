using System;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainBaseEntity
    {
        
    }
    
    
    public class OrderItemChoice<TKey> : IDomainBaseEntity<TKey>
        where TKey: IEquatable<TKey>
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