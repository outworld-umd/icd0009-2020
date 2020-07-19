using System;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid, AppUser>, IDomainBaseEntity
    {
        
    }
    
    public class NutritionInfo<TKey, TUser> : IDomainBaseEntity<TKey>
        where TKey : IEquatable<TKey>
        where TUser : AppUser<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Unit { get; set; } = default!;
    }
}