using System;
using System.Collections.Generic;
using Domain.App.Enums;
using Domain.App.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App
{
    public class Quiz : Quiz<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
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

        public QuizType QuizType { get; set; } = default!;

        public ICollection<QuizSession>? QuizSessions { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}