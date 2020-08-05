using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RestaurantUserService : BaseEntityService<IAppUnitOfWork, IRestaurantUserRepository, IRestaurantUserServiceMapper, DAL.App.DTO.RestaurantUser, RestaurantUser>, IRestaurantUserService
    {
        public RestaurantUserService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.RestaurantUsers, new RestaurantUserServiceMapper())
        {
        }
    }
}