using System;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Combined;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainEntityIdMetadata
    {
    }
    
    
    public class OrderItemChoice<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public TKey OrderRowId { get; set; } = default!; // ZDES BIL 0!!!!!!!!!!!!!!!
        public OrderRow? OrderRow { get; set; }

        public Guid? ItemChoiceId { get; set; } = default!;
        public ItemChoice? ItemChoice { get; set; }
        
        public string? Name { get; set; } = default!;
        

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}