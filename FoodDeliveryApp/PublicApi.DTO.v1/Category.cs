using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class Category : Category<Guid>, IDomainEntityId
    {
    }

    public class Category<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public ICollection<RestaurantView>? Restaurants { get; set; }

        public TKey Id { get; set; } = default!;
    }
}