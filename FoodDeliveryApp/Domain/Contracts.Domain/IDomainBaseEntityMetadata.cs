using System;

namespace Contracts.Domain
{
    public interface IDomainEntityBaseMetadata : IDomainEntityBaseMetadata<Guid>
    {
    }

    public interface IDomainEntityBaseMetadata<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
    }
}