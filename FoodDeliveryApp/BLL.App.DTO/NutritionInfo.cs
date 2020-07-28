using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class NutritionInfo<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MinLength(2)] [MaxLength(64)] [Required] public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(7,3)")] [Required] public decimal Amount { get; set; }
        [MinLength(2)] [MaxLength(64)] [Required] public string Unit { get; set; } = default!;
        
        public TKey ItemId { get; set; } = default!;
        public Item? Item { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}