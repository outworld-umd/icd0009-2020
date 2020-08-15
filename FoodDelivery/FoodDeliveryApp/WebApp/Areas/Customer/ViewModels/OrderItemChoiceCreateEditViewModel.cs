using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Customer.ViewModels {

    public class OrderItemChoiceCreateEditViewModel {
        public OrderItemChoice OrderItemChoice { get; set; } = default!;
        public SelectList? OrderRows { get; set; }
        public SelectList? ItemChoices { get; set; }
    }

}