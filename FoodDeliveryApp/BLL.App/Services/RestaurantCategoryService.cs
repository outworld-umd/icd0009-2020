using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RestaurantCategoryService : BaseEntityService<IAppUnitOfWork, IRestaurantCategoryRepository, IRestaurantCategoryServiceMapper, DAL.App.DTO.RestaurantCategory, RestaurantCategory>, IRestaurantCategoryService
    {
        public RestaurantCategoryService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.RestaurantCategories, new RestaurantCategoryServiceMapper())
        {
        }
        
        public async Task<IEnumerable<Restaurant>> GetAllByUser(Guid userId, bool noTracking = true) {
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Select(rc => rc.Restaurant).Where(r => restaurantIds.Contains(r!.Id))!;
        }
    }
}