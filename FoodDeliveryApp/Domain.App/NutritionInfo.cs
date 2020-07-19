using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using DAL.Base;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class NutritionInfo : NutritionInfo<Guid, AppUser>, IDomainBaseEntityMetadata {
        
    }
    
    public class NutritionInfo<TKey, TUser> : DomainBaseEntityMetadata<TKey>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(7,3)")]public decimal Amount { get; set; }
        [MinLength(2)] [MaxLength(64)] public string Unit { get; set; } = default!;
        
    }
}