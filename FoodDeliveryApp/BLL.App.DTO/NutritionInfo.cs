using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class NutritionInfo : NutritionInfo<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class NutritionInfo<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(7,3)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Amount), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public decimal Amount { get; set; }
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Unit), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public string Unit { get; set; } = default!;
        
        public TKey ItemId { get; set; } = default!;
        [Display(Name = nameof(Item), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public Item? Item { get; set; }
        
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.NutritionInfo.NutritionInfo))]
        public DateTime ChangedAt { get; set; }
    }
}