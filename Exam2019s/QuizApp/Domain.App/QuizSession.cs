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
    public class QuizSession : QuizSession<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
        public Guid? QuizId { get; set; }
        public Quiz? Quiz { get; set; }
    }

    public class QuizSession<TKey, TUser> : DomainEntityIdMetadataUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public ICollection<Answer>? Answers { get; set; }
    }
}