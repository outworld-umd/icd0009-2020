using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemOption
    {
        public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }
        
        public ICollection<ItemChoice>? ItemChoices { get; set; }
        
        public Guid Id { get; set; } = default!;
    }
}