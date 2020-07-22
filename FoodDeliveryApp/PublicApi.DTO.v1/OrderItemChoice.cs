using System;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainEntityId
    {
    }
    
    
    public class OrderItemChoice<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public Guid? OrderRowId { get; set; } // ZDES BIL 0!!!!!!!!!!!!!!!

        public TKey ItemChoiceId { get; set; } = default!;
        public string? ItemChoiceName { get; set; }

        public TKey Id { get; set; } = default!;
    }
}