using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class NutritionInfo : NutritionInfo<Guid, AppUser>, IDomainEntityBaseMetadata, IDomainEntityUser<AppUser> {
        
    }
    
    public class NutritionInfo<TKey, TUser> : DomainEntityBaseMetadata<TKey>, IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey> 
        where TUser : AppUser<TKey> {
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        [MinLength(2)] [MaxLength(64)] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(7,3)")]public decimal Amount { get; set; }
        [MinLength(2)] [MaxLength(64)] public string Unit { get; set; } = default!;
        public TKey AppUserId { get; set; } = default!;
        public TUser? AppUser { get; set; }
    }
}