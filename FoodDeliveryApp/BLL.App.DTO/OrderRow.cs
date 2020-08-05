using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Combined;

namespace BLL.App.DTO
{
    public class OrderRow : OrderRow<Guid>, IDomainEntityIdMetadata
    {
    }
    
    public class OrderRow<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public Guid? ItemId { get; set; } = default!;
        [Display(Name = nameof(Item), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public Item? Item { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public string Name { get; set; } = default!;

        public TKey OrderId { get; set; } = default!;
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public Order? Order { get; set; }
        [Range(1, 20)] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Amount), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public decimal Cost { get; set; }
        [MaxLength(512, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common.Common))]
        [Display(Name = nameof(Comment), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public string? Comment { get; set; }
        public ICollection<OrderItemChoice>? OrderItemChoices { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.OrderRow.OrderRow))]
        public DateTime ChangedAt { get; set; }
    }
}