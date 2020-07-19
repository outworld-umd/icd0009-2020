using System;

namespace Contracts.Domain
{
    public interface IDomainBaseEntity : IDomainBaseEntity<Guid>
    {
    }

    public interface IDomainBaseEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}