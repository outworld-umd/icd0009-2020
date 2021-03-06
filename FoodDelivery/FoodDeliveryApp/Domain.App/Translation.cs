using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace Domain.App
{
    public class Translation : Translation<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Translation<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(5)] public string Culture { get; set; } = default!;
        [MaxLength(10240)] public string Value { get; set; } = default!;

        public TKey LangStringId { get; set; } = default!;
        public LangString? LangString { get; set; } = default!;
    }
}