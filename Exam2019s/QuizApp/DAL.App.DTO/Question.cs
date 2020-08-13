using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ee.itcollege.anguzo.Domain.Base;

namespace DAL.App.DTO
{
    public class Question : Question<Guid>
    {
        public Guid? CorrectChoiceId { get; set; }
        [ForeignKey(nameof(CorrectChoiceId))] 
        public Choice? CorrectChoice { get; set; }
    }

    public class Question<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public TKey QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }

        public ICollection<Choice>? Choices { get; set; }
        public int? TimesAnswered => Choices.Select(c => c.Answers!.Count).Sum();
    }
}