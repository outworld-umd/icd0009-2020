using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemOption : ItemOption<Guid>, IDomainBaseEntityMetadata
    {
    }

    public class ItemOption<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
    {

        public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }


        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public ICollection<ItemChoice>? ItemChoices { get; set; }
    }
}