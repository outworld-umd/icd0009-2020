using System.ComponentModel.DataAnnotations;

namespace Domain.App.Enums
{
    public enum OrderStatus
    {
        [Display(Name = nameof(Unfinished), ResourceType = typeof(Resources.Enums.OrderStatus.OrderStatus))]
        Unfinished,
        [Display(Name = nameof(Cooking), ResourceType = typeof(Resources.Enums.OrderStatus.OrderStatus))]
        Cooking,
        [Display(Name = nameof(Delivering), ResourceType = typeof(Resources.Enums.OrderStatus.OrderStatus))]
        Delivering,
        [Display(Name = nameof(Delivered), ResourceType = typeof(Resources.Enums.OrderStatus.OrderStatus))]
        Delivered
    }
}