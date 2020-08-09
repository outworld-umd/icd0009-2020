using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Contracts.Domain.Base.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ItemOption : ItemOption<Guid>, IDomainEntityIdMetadata 
    {
    }

    public class ItemOption<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public string Name { get; set; } = default!;
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(IsRequired), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public bool IsRequired { get; set; }
        
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(IsSingle), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public bool IsSingle { get; set; }
        
        public TKey ItemId { get; set; } = default!;
        [Display(Name = nameof(Item), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public Item? Item { get; set; }
        
        public ICollection<ItemChoice>? ItemChoices { get; set; }
        
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemOption.ItemOption))]
        public DateTime ChangedAt { get; set; }
    }
}