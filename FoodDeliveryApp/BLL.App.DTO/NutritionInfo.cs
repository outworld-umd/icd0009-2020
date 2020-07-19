using System;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Domain.Base;

namespace BLL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class NutritionInfo<TKey> : DomainBaseEntityMetadata<TKey>
        where TKey: IEquatable<TKey>
    {
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
    }
}