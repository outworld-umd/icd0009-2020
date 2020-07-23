using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RestaurantService : BaseEntityService<IAppUnitOfWork, IRestaurantRepository, IRestaurantServiceMapper, DAL.App.DTO.Restaurant, Restaurant>, IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Restaurants, new RestaurantServiceMapper())
        {
        }
    }
}