using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace Domain.App
{
    public class ItemInType : ItemInType<Guid>, IDomainEntityIdMetadata
    {
    }

    public class ItemInType<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemTypeId { get; set; } = default!;
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
}