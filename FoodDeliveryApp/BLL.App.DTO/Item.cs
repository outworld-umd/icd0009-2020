using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Item : Item<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class Item<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(2)] [Required] public string Name { get; set; } = default!;
        [MaxLength(512)] public string? PictureLink { get; set; }
        [Column(TypeName = "decimal(6,2)")] [Required] public decimal Price { get; set; }
        [MaxLength(512)] public string? Description { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
        
        public TKey Id { get; set; } = default!;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}