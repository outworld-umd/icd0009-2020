using System;
using Contracts.Domain.Base.Basic;

namespace Contracts.Domain.Base.Combined
{
    public interface IDomainEntityIdMetadata : IDomainEntityIdMetadata<Guid>
    {
    }

    public interface IDomainEntityIdMetadata<TKey> : IDomainEntityId<TKey>, IDomainEntityMetadata
        where TKey : IEquatable<TKey>
    {
    }
}