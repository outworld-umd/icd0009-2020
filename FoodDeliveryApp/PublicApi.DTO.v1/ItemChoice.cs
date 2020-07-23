using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{

    
    public class ItemChoice
    {
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }

        public Guid Id { get; set; } = default!;
    }
}