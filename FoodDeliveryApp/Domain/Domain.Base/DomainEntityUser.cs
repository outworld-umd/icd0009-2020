using System;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Domain.Base
{
    public class DomainEntityUser
    {
        public abstract class DomainEntityIdUser<TUser> : DomainEntityIdUser<Guid, TUser>,IDomainBaseEntity, IDomainEntityUser<TUser>
            where TUser : IdentityUser<Guid>
        {
        }

        public abstract class DomainEntityIdUser<TKey, TUser> : DomainBaseEntity<TKey>,
            IDomainEntityUser<TKey, TUser>
            where TKey : IEquatable<TKey>
            where TUser: IdentityUser<TKey>
        {
            public TKey AppUserId { get; set; } = default!;

            [JsonIgnore] public TUser AppUser { get; set; } = default!;
        }
    }
}