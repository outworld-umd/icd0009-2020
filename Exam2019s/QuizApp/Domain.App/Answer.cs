using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Answer : Answer<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
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