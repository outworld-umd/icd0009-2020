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
    public class Answer : Answer<Guid, AppUser>, IDomainEntityIdMetadata
    {
    }

    public class Answer<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ChoiceId { get; set; } = default!;
        public Choice? Choice { get; set; }

        public TKey QuizSessionId { get; set; } = default!;
        public QuizSession QuizSession { get; set; } = default!;

        public bool IsCorrect { get; set; }
    }
}