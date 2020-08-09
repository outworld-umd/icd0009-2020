using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base.Basic;
using Contracts.Domain.Base.Combined;

namespace BLL.App.DTO
{
    public class Translation : Translation<Guid>, IDomainEntityIdMetadata
    {
        
    }
    public class Translation<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(5, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = "CultureString", ResourceType = typeof(Resources.BLL.App.DTO.Translation.Translation))]
        public string Culture { get; set; } = default!;
        
        [MaxLength(10240, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = nameof(Value), ResourceType = typeof(Resources.BLL.App.DTO.Translation.Translation))]
        public string Value { get; set; } = default!;

        [Display(Name = nameof(LangStringId), ResourceType = typeof(Resources.BLL.App.DTO.Translation.Translation))]
        public TKey LangStringId { get; set; } = default!;
        [Display(Name = nameof(LangString), ResourceType = typeof(Resources.BLL.App.DTO.Translation.Translation))]
        public LangString? LangString { get; set; }
        
        public TKey Id { get; set; } = default!;
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemType.ItemType))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemType.ItemType))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemType.ItemType))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemType.ItemType))]
        public DateTime ChangedAt { get; set; }
    }
}