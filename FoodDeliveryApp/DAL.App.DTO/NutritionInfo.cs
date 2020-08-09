using System;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Domain.Base;
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