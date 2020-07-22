using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemChoice : ItemChoice<Guid>, IDomainEntityId 
    {
    }
    
    public class ItemChoice<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }

        public TKey Id { get; set; } = default!;
    }
}