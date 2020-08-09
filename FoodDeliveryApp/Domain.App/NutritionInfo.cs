using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
using ee.itcollege.anguzo.Domain.Identity;namespace Domain.App
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainEntityIdMetadata {
        
    }
    
    public class NutritionInfo<TKey> : DomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey> 
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(7,3)")] [Required] public decimal Amount { get; set; }
        [MinLength(2)] [MaxLength(64)] [Required] public string Unit { get; set; } = default!;
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
    }
}