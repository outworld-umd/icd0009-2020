using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.Domain;
using Domain.App.Identity;

namespace Contracts.DAL.App {

    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker {

        public IAddressRepository Addresses { get; }
        public ICategoryRepository Categories { get; }
        public IItemRepository Items { get; }
        public IItemChoiceRepository ItemChoices { get; }
        public IItemInTypeRepository ItemInTypes { get; }
        public IItemOptionRepository ItemOptions { get; }
        public IItemTypeRepository ItemTypes { get; }
        public INutritionInfoRepository NutritionInfos { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemChoiceRepository OrderItemChoices { get; }
        public IOrderRowRepository OrderRows { get; }
        public IRestaurantRepository Restaurants { get; }
        public IRestaurantUserRepository RestaurantUsers { get; }
        public IRestaurantCategoryRepository RestaurantCategories { get; }
        public IWorkingHoursRepository WorkingHourses { get; }
        public ILangStringRepository LangStrings { get; }
        public ITranslationRepository Translations { get; }



    }

}