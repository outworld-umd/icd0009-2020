using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

namespace PublicApi.DTO.v1
{
    public class ItemChoice
    {
        public string Name { get; set; } = default!;
        public decimal AdditionalPrice { get; set; }

        public Guid Id { get; set; } = default!;
        public Guid ItemOptionId { get; set; } = default!;

    }
}