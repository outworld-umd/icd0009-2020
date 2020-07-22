using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemType : ItemType<Guid>, IDomainEntityId
    {
    }
    
    public class ItemType<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }

        public ICollection<ItemView>? Items { get; set; }

        public TKey Id { get; set; } = default!;
    }
}