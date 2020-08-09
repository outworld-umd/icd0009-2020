using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Combined;
using ee.itcollege.anguzo.Domain.Base;
namespace BLL.App.DTO
{
    public class OrderItemChoice : OrderItemChoice<Guid>, IDomainEntityIdMetadata
    {
    }
    
    
    public class OrderItemChoice<TKey> : IDomainEntityIdMetadata<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        public TKey OrderRowId { get; set; } = default!;

        [Display(Name = nameof(OrderRow), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public OrderRow? OrderRow { get; set; }

        public Guid? ItemChoiceId { get; set; } = default!;
        [Display(Name = nameof(ItemChoice), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public ItemChoice? ItemChoice { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public string? Name { get; set; } = default!;
        
        [Range(1, 20)] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))]         
        [Display(Name = nameof(Amount), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public int Amount { get; set; }
        [Column(TypeName = "decimal(6,2)")] 
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common.Common))] 
        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public decimal Cost { get; set; }

        [Display(Name = nameof(CreatedBy), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public string? CreatedBy { get; set; }
        [Display(Name = nameof(CreatedAt), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public DateTime CreatedAt { get; set; }
        [Display(Name = nameof(ChangedBy), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public string? ChangedBy { get; set; }
        [Display(Name = nameof(ChangedAt), ResourceType = typeof(Resources.BLL.App.DTO.OrderItemChoice.OrderItemChoice))]
        public DateTime ChangedAt { get; set; }
    }
}