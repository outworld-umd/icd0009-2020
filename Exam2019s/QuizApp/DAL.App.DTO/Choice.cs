using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace DAL.App.DTO
{
    public class Choice : Choice<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Choice<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        [Display(Name = "Value")] [Required] public string Value { get; set; } = default!;

        [Display(Name = "Question")]
        [Required]
        public TKey QuestionId { get; set; } = default!;

        [InverseProperty("Choices")] public Question? Question { get; set; }
        [Display(Name = "Answers")] public ICollection<Answer>? Answers { get; set; }
        [Display(Name = "Times Answered")] public int TimesAnswered => Answers?.Count ?? 0;
    }
}