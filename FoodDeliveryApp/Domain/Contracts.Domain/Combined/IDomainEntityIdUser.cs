using System;
using Contracts.Domain.Basic;
using Microsoft.AspNetCore.Identity;

namespace Contracts.Domain.Combined
{
    public interface IDomainEntityIdUser<TUser>: IDomainEntityUser<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
    }
    
    public interface IDomainEntityIdUser<TKey, TUser> : IDomainEntityId<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
    }
}