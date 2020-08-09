using Contracts.DAL.App.Repositories;
using ee.itcollege.anguzo.Contracts.DAL.Base;
using ee.itcollege.anguzo.Contracts.Domain.Base;

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