using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace Domain.App
{
    public class Category : Category<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Category<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey NameId { get; set; } = default!;

        [MaxLength(64)]
        [MinLength(2)]
        [Required]
        public LangString Name { get; set; } = default!;

        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
    }
}