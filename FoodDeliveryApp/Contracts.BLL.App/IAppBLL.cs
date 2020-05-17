using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAddressService Addresses { get; }
        public ICategoryService Categories { get; }
        public IItemChoiceService ItemChoices { get; }
        public IItemInTypeService ItemInTypes { get; }
        public IItemOptionService ItemOptions { get; }
        public IItemService Items { get; }
        public IItemTypeService ItemTypes { get; }
        public INutritionInfoService NutritionInfos { get; }
        public IOrderItemChoiceService OrderItemChoices { get; }
        public IOrderRowService OrderRows { get; }
        public IOrderService Orders { get; }
        public IRestaurantCategoryService RestaurantCategories { get; }
        public IRestaurantService Restaurants { get; }
        public IRestaurantUserService RestaurantUsers { get; }
        public IWorkingHoursService WorkingHourses { get; }
        
    }
}