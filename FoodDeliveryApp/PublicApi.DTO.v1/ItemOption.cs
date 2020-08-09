using System;
using System.Collections.Generic;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemOption
    {
        public string Name { get; set; } = default!;
        public bool IsRequired { get; set; }
        public bool IsSingle { get; set; }
        
        public ICollection<ItemChoice>? Choices { get; set; }
        
        public Guid Id { get; set; } = default!;
        public Guid ItemId { get; set; } = default!;

    }
}