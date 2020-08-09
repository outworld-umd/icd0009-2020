using System;
using System.Collections.Generic;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
namespace PublicApi.DTO.v1
{
    public class OrderRow
    {
        public Guid? ItemId { get; set; } = default!;        
        public string? Name { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? Choices { get; set; }
        
        public Guid Id { get; set; } = default!;
    }
}