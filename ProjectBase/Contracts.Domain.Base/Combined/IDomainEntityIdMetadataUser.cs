using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using Microsoft.AspNetCore.Identity;

namespace ee.itcollege.anguzo.Contracts.Domain.Base.Combined
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