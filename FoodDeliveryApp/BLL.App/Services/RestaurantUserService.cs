using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RestaurantUserService : BaseEntityService<IRestaurantUserRepository, IAppUnitOfWork, DAL.App.DTO.RestaurantUser, RestaurantUser>, IRestaurantUserService
    {
        public RestaurantUserService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.RestaurantUser, RestaurantUser>(), unitOfWork.RestaurantUsers)
        {
        }
    }
}