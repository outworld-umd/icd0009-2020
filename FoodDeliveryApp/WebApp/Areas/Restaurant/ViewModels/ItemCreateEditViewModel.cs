using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Restaurant.ViewModels
{
    public class ItemCreateEditViewModel
    {
        public Item Item { get; set; } = default!;
        public SelectList? Restaurants { get; set; }
    }
}