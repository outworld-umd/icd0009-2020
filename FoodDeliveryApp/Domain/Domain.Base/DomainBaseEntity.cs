using System;
using Contracts.Domain;
using Contracts.Domain;

namespace Domain.Base
{
    public abstract class DomainBaseEntity : DomainBaseEntity<Guid>, IDomainBaseEntity
    {
    }
    
    public abstract class DomainBaseEntity<TKey> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}