using System;
using System.Collections.Generic;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;

namespace PublicApi.DTO.v1
{
    public class OrderRow
    {
        public Guid ItemId { get; set; } = default!;
        public string? ItemName { get; set; }
        public Guid OrderId { get; set; } = default!;
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
        public Guid Id { get; set; } = default!;
    }
}