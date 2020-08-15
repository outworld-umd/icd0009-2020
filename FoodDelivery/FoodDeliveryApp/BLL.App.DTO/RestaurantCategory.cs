using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class RestaurantCategory : RestaurantCategory<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class RestaurantCategory<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        
        public TKey CategoryId { get; set; } = default!;
        [Display(Name = "Category", ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public Category? Category { get; set; }

        public TKey RestaurantId { get; set; } = default!;
        [Display(Name = "Restaurant", ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public Restaurant? Restaurant { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.RestaurantCategory.RestaurantCategory))]
        public DateTime ChangedAt { get; set; }
    }
}