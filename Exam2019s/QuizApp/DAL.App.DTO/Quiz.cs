using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App.Enums;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO
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
        [DisplayName("Title")] [Required] public string Title { get; set; } = default!;
        [DisplayName("Description")] public string? Description { get; set; }

        [DisplayName("Quiz Type")] [Required] public QuizType QuizType { get; set; } = default!;

        [DisplayName("QuizSessions")] public ICollection<QuizSession>? QuizSessions { get; set; }
        [DisplayName("Questions")] public ICollection<Question>? Questions { get; set; }
        [DisplayName("Times Taken")] public int TimesTaken => QuizSessions?.Count ?? 0;
    }
}