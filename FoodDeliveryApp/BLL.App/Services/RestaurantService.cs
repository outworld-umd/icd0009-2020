using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RestaurantService : BaseEntityService<IRestaurantRepository, IAppUnitOfWork, DAL.App.DTO.Restaurant, Restaurant>, IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.Restaurant, Restaurant>(), unitOfWork.Restaurants)
        {
        }
    }
}