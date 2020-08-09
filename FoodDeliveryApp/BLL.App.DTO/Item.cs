using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class Item : Item<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class Item<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [MinLength(2, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public string Name { get; set; } = default!;
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(PictureLink), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public string? PictureLink { get; set; }
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public decimal Price { get; set; }
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public string? Description { get; set; }
        public Guid? RestaurantId { get; set; }
        [Display(Name = nameof(Restaurant), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public Restaurant? Restaurant { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
        public ICollection<ItemInType>? ItemInTypes { get; set; }
        public ICollection<NutritionInfo>? NutritionInfos { get; set; }
        public ICollection<ItemOption>? ItemOptions { get; set; }
        
        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.Item.Item))]
        public DateTime ChangedAt { get; set; }
    }
}