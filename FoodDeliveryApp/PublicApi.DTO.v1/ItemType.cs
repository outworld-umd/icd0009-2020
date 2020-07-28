using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemType
    {
        public string Name { get; set; } = default!;
        public bool IsSpecial { get; set; }
        public string? Description { get; set; }
        public Guid RestaurantId { get; set; } = default!;

        public ICollection<ItemView>? Items { get; set; }

        public Guid Id { get; set; } = default!;
    }
}