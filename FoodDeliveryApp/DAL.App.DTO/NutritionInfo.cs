using System;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Combined;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class NutritionInfo<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}