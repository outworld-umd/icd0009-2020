using System;
using Contracts.Domain.Basic;

namespace Contracts.Domain.Combined
{
    public interface IDomainEntityIdMetadata : IDomainEntityIdMetadata<Guid>
    {
    }

    public interface IDomainEntityIdMetadata<TKey> : IDomainEntityId<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
    }
}