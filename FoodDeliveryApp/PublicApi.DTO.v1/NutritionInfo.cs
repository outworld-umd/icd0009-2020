using System;
using Contracts.Domain.Basic;

namespace PublicApi.DTO.v1
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainEntityId
    {
    }
    
    public class NutritionInfo<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
        
        public TKey Id { get; set; } = default!;
    }
}