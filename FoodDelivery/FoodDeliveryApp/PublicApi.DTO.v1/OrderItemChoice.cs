using System;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
namespace PublicApi.DTO.v1
{
    public class OrderItemChoice
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }

        public Guid OrderRowId { get; set; } // ZDES BIL 0!!!!!!!!!!!!!!!

        public Guid? ItemChoiceId { get; set; } = default!;
        public string Name { get; set; } = default!;

        public Guid Id { get; set; } = default!;
    }
}