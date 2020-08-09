using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ee.itcollege.anguzo.Domain.Base
{
    public abstract class DomainEntityIdUser<TUser> : DomainEntityIdUser<Guid, TUser>, IDomainEntityId, IDomainEntityUser<TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    public abstract class DomainEntityIdUser<TKey, TUser> : DomainEntityId<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; } = default!;

        [JsonIgnore] public TUser? AppUser { get; set; }
    }
}