using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid, AppUser>, IDomainBaseEntityMetadata
    {
        
    }
    
    public class NutritionInfo<TKey, TUser> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
    }
}