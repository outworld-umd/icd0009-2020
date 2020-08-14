using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace DAL.App.DTO
{
    public class Question : Question<Guid>, IDomainEntityIdMetadata
    {
        public Guid? CorrectChoiceId { get; set; }
        [ForeignKey(nameof(CorrectChoiceId))] 
        [DisplayName("Correct Choice")] public Choice? CorrectChoice { get; set; }
    }

    public class Question<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        [DisplayName("Title")] [Required] public string Title { get; set; } = default!;

        [DisplayName("Description")] public string? Description { get; set; }
        [DisplayName("Quiz")] [Required] public TKey QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        [DisplayName("Choices")] public ICollection<Choice>? Choices { get; set; }
        [DisplayName("Times Answered")] public int TimesAnswered => Choices?.Select(c => c.TimesAnswered).Sum() ?? 0;
    }
}