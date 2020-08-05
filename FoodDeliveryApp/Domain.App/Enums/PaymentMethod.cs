using System.ComponentModel.DataAnnotations;

namespace Domain.App.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = nameof(Cash), ResourceType = typeof(Resources.Enums.PaymentMethod.PaymentMethod))]
        Cash,
        [Display(Name = nameof(Card), ResourceType = typeof(Resources.Enums.PaymentMethod.PaymentMethod))]
        Card,
        [Display(Name = nameof(InApp), ResourceType = typeof(Resources.Enums.PaymentMethod.PaymentMethod))]
        InApp
    }
}