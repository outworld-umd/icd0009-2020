using System;
using Contracts.Domain;
using Contracts.Domain.Basic;

namespace DAL.App.DTO.Identity
{
    public class AppRole : AppRole<Guid>
    {
    }

    public class AppRole<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}