using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Basic;

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