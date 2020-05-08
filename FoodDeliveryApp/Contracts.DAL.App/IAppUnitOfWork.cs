using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App {

    public interface IAppUnitOfWork : IBaseUnitOfWork {

        public IAddressRepository Addresses { get; }
        public ICategoryRepository Categories { get; }
        public ICustomerRepository Customers { get; }
        public IItemRepository Items { get; }
        public IItemChoiceRepository ItemChoices { get; }
        public IItemOptionRepository ItemOptions { get; }
        public IItemTypeRepository ItemTypes { get; }
        public INutritionInfoRepository NutritionInfos { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemChoiceRepository OrderItemChoices { get; }
        public IOrderRowRepository OrderRows { get; }
        public IRestaurantRepository Restaurants { get; }
        public IRestaurantCategoryRepository RestaurantCategories { get; }
        public IWorkingHoursRepository WorkingHourses { get; }

    }

}