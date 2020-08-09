using System;
using System.Collections.Generic;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
namespace DAL.App.DTO {

    public class Category : Category<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Category<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {        
        public TKey NameId { get; set; } = default!;

        public string Name { get; set; } = default!;
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }

        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }

}