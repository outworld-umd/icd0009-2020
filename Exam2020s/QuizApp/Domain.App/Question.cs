using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace Domain.App
{
    public class Question : Question<Guid>, IDomainEntityIdMetadata
    {
        public Guid? CorrectChoiceId { get; set; }
        [ForeignKey(nameof(CorrectChoiceId))] public Choice? CorrectChoice { get; set; }
    }

    public class Question<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public TKey QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        public ICollection<Choice>? Choices { get; set; }
    }
}