using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace DAL.App.DTO
{
    public class Answer : Answer<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Answer<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [DisplayName("Choice")] public TKey ChoiceId { get; set; } = default!;
        public Choice? Choice { get; set; }

        [DisplayName("Quiz Session")]
        [Required]
        public TKey QuizSessionId { get; set; } = default!;

        public QuizSession QuizSession { get; set; } = default!;
        [DisplayName("Is Correct")] public bool IsCorrect { get; set; }
    }
}