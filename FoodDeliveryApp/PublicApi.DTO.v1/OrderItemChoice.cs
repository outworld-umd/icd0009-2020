using System;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class OrderItemChoice
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public Guid? OrderRowId { get; set; } // ZDES BIL 0!!!!!!!!!!!!!!!

        public Guid ItemChoiceId { get; set; } = default!;
        public string? ItemChoiceName { get; set; }

        public Guid Id { get; set; } = default!;
    }
}