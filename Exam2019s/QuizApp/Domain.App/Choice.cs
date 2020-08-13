using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace Domain.App
{
    public class Choice : Choice<Guid>, IDomainEntityIdMetadata
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

    }
}