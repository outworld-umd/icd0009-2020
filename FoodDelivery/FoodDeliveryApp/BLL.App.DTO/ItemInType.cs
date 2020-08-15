using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.DTO.Identity;
using ee.itcollege.anguzo.Contracts.Domain.Base;
using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;

namespace BLL.App.DTO
{
    public class ItemInType : ItemInType<Guid>, IDomainEntityIdMetadata 
    {
    }
    
    public class ItemInType<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    { 
        public TKey Id { get; set; } = default!;
        
        public TKey ItemTypeId { get; set; } = default!;
        [Display(Name = nameof(ItemType), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public ItemType? ItemType { get; set; }

        public TKey ItemId { get; set; } = default!;
        [Display(Name = nameof(Item), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public Item? Item { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.ItemInType.ItemInType))]
        public DateTime ChangedAt { get; set; }
    }
}