using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using Contracts.Domain;
using Contracts.Domain.Base.Basic;
namespace PublicApi.DTO.v1.Identity
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