using System;

namespace Contracts.Domain
{
    public interface IDomainBaseEntityMetadata : IDomainEntityBaseMetadata<Guid>
    {
    }

    public interface IDomainEntityBaseMetadata<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
    }
}