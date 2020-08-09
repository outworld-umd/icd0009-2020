using System;
using System.Threading.Tasks;
using BLL.App.Services;
using ee.itcollege.anguzo.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using ee.itcollege.anguzo.Contracts.BLL.Base;


using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IAddressService Addresses => GetService<IAddressService>(() => new AddressService(UnitOfWork));
        
        public ICategoryService Categories => GetService<ICategoryService>(() => new CategoryService(UnitOfWork));
        public IItemChoiceService ItemChoices => GetService<IItemChoiceService>(() => new ItemChoiceService(UnitOfWork));
        public IItemInTypeService ItemInTypes => GetService<IItemInTypeService>(() => new ItemInTypeService(UnitOfWork));
        public IItemOptionService ItemOptions => GetService<IItemOptionService>(() => new ItemOptionService(UnitOfWork));
        public IItemService Items => GetService<IItemService>(() => new ItemService(UnitOfWork));
        public IItemTypeService ItemTypes => GetService<IItemTypeService>(() => new ItemTypeService(UnitOfWork));
        public INutritionInfoService NutritionInfos => GetService<INutritionInfoService>(() => new NutritionInfoService(UnitOfWork));
        public IOrderItemChoiceService OrderItemChoices => GetService<IOrderItemChoiceService>(() => new OrderItemChoiceService(UnitOfWork));
        public IOrderRowService OrderRows => GetService<IOrderRowService>(() => new OrderRowService(UnitOfWork));
        public IOrderService Orders => GetService<IOrderService>(() => new OrderService(UnitOfWork));
        public IRestaurantCategoryService RestaurantCategories => GetService<IRestaurantCategoryService>(() => new RestaurantCategoryService(UnitOfWork));
        public IRestaurantService Restaurants => GetService<IRestaurantService>(() => new RestaurantService(UnitOfWork));
        public IRestaurantUserService RestaurantUsers => GetService<IRestaurantUserService>(() => new RestaurantUserService(UnitOfWork));
        public IWorkingHoursService WorkingHourses => GetService<IWorkingHoursService>(() => new WorkingHoursService(UnitOfWork));
        public ILangStringService LangStrings => GetService<ILangStringService>(() => new LangStringService(UnitOfWork));
        public ITranslationService Translations => GetService<ITranslationService>(() => new TranslationService(UnitOfWork));
    }
}