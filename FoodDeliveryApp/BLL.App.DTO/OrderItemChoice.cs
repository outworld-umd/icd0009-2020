using System;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    
    public class OrderItemChoice<TKey> : DomainEntityIdMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public TKey OrderRowId { get; set; } = default!; // ZDES BIL 0!!!!!!!!!!!!!!!
        public OrderRow? OrderRow { get; set; }

        public TKey ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }

    }
}