using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class ItemChoice : ItemChoice<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemChoice<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public string Name { get; set; } = default!;
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(AdditionalPrice), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public decimal AdditionalPrice { get; set; }

        public TKey ItemOptionId { get; set; } = default!;
        [Display(Name = nameof(ItemOption), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public ItemOption? ItemOption { get; set; }
        
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }
        
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemChoice.ItemChoice))]
        public DateTime ChangedAt { get; set; }
    }
}