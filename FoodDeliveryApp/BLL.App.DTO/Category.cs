using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Combined;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Category : Category<Guid>, IDomainEntityIdMetadata
    {
    }

    public class Category<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
        public TKey NameId { get; set; } = default!;

        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Category.Category))]
        public string Name { get; set; } = default!;
        public ICollection<RestaurantCategory>? RestaurantCategories { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.Category.Category))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.Category.Category))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.Category.Category))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.Category.Category))]
        public DateTime ChangedAt { get; set; }
    }
}