using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels {

    public class OrderRowCreateEditViewModel {
        public OrderRow OrderRow { get; set; } = default!;
        public SelectList? Items { get; set; }
        public SelectList? Orders { get; set; }
    }

}