using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF {

    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork {
        
        public IAddressRepository Addresses => GetRepository(() => new AddressRepository(UOWDbContext));
        public ICategoryRepository Categories => GetRepository(() => new CategoryRepository(UOWDbContext));
        public IItemRepository Items => GetRepository(() => new ItemRepository(UOWDbContext));
        public IItemChoiceRepository ItemChoices => GetRepository(() => new ItemChoiceRepository(UOWDbContext));
        public IItemInTypeRepository ItemInTypes => GetRepository(() => new ItemInTypeRepository(UOWDbContext));
        public IItemOptionRepository ItemOptions => GetRepository(() => new ItemOptionRepository(UOWDbContext));
        public IItemTypeRepository ItemTypes => GetRepository(() => new ItemTypeRepository(UOWDbContext));
        public INutritionInfoRepository NutritionInfos => GetRepository(() => new NutritionInfoRepository(UOWDbContext));
        public IOrderRepository Orders => GetRepository(() => new OrderRepository(UOWDbContext));
        public IOrderItemChoiceRepository OrderItemChoices => GetRepository(() => new OrderItemChoiceRepository(UOWDbContext));
        public IOrderRowRepository OrderRows => GetRepository(() => new OrderRowRepository(UOWDbContext));
        public IRestaurantRepository Restaurants => GetRepository(() => new RestaurantRepository(UOWDbContext));
        public IRestaurantCategoryRepository RestaurantCategories => GetRepository(() => new RestaurantCategoryRepository(UOWDbContext));
        public IWorkingHoursRepository WorkingHourses => GetRepository(() => new WorkingHoursRepository(UOWDbContext));
        
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext) 
        {
            
        }

    }

}