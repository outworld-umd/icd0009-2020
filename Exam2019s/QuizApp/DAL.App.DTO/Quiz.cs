using System;
using System.Collections.Generic;
using DAL.App.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;
using Type = Domain.App.Enums.Type;

namespace DAL.App.DTO
{
    public class Quiz : Quiz<Guid, AppUser>
    {
        public new Guid? AppUserId { get; set; }
        public new AppUser? AppUser { get; set; }
    }

    public class Quiz<TKey, TUser> : DomainEntityIdMetadataUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public Type Type { get; set; } = default!;

        public ICollection<Question>? Questions { get; set; }
    }
}