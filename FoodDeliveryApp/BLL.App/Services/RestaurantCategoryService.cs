using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RestaurantCategoryService : BaseEntityService<IRestaurantCategoryRepository, IAppUnitOfWork, DAL.App.DTO.RestaurantCategory, RestaurantCategory>, IRestaurantCategoryService
    {
        public RestaurantCategoryService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.RestaurantCategory, RestaurantCategory>(), unitOfWork.RestaurantCategories)
        {
        }
    }
}