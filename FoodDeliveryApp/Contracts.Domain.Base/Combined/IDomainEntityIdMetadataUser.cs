using System;
using Microsoft.AspNetCore.Identity;
using Contracts.Domain.Base.Basic;

namespace Contracts.Domain.Base.Combined
{
    
    public interface IDomainEntityIdMetadataUser<TUser>: IDomainEntityIdMetadataUser<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
    }
    
    public interface IDomainEntityIdMetadataUser<TKey, TUser> : IDomainEntityId<TKey>, IDomainEntityMetadata, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
    }
}