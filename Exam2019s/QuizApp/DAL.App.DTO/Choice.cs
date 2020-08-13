using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Domain.Base;

namespace DAL.App.DTO
{
    public class Choice : Choice<Guid>
    {
    }

    public class Choice<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        public string Value { get; set; } = default!;

        public TKey QuestionId { get; set; } = default!;
        [InverseProperty("Choices")]
        public Question? Question { get; set; }

        public ICollection<Answer>? Answers { get; set; }
        public int? TimesAnswered => Answers!.Count;

    }
}