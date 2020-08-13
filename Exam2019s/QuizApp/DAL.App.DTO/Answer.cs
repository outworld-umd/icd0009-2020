using System;
using DAL.App.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO
{
    public class Answer : Answer<Guid, AppUser>
    {
    }

    public class Answer<TKey, TUser> : DomainEntityIdMetadataUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey ChoiceId { get; set; } = default!;
        public Choice? Choice { get; set; }

    }
}