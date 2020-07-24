using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class NutritionInfo
    {
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
        
        public Guid Id { get; set; } = default!;
    }
}