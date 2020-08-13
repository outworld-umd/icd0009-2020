using System;
using System.Collections.Generic;
using DAL.App.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO
{
    public class QuizSession : QuizSession<Guid, AppUser>, IDomainEntityIdMetadataUser<AppUser>
    {
    }

    public class QuizSession<TKey, TUser> : DomainEntityIdMetadataUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        
        public ICollection<Answer>? Answers { get; set; }

    }
}