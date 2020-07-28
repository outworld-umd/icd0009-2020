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
        // TODO AnyAsyncForRU
        // public async Task<bool> AnyAsync(Expression<Func<RestaurantUser, bool>> predicate, object? userId = null)
        // {
        //     var result = await ServiceRepository.AnyAsync(predicate, userId);
        //     return result; 
        // }
        //
        // public bool Any(Expression<Func<RestaurantUser, bool>> predicate, object? userId = null)
        // {
        //     var result = ServiceRepository.Any(predicate, userId);
        //     return result; 
        // }
    }
}