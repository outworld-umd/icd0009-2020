using System;
using Contracts.Domain;
using Contracts.Domain.Basic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Domain;

namespace Domain.Base
{
    public abstract class DomainEntityIdMetadataUser<TUser> : DomainEntityIdMetadataUser<Guid, TUser>, IDomainEntityUser<TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    public abstract class DomainEntityIdMetadataUser<TKey, TUser> : DomainEntityIdMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser: IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;

        [JsonIgnore] public TUser? AppUser { get; set; }
    }
}