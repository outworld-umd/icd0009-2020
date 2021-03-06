using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.anguzo.BLL.Base.Services;using Contracts.BLL.App.Mappers;
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

        public async Task<IEnumerable<Restaurant>> GetAllSortedByDeliveryAsync(object? userId = null, bool noTracking = true) {
            return (await base.GetAllAsync(userId, noTracking)).OrderBy(e => e.DeliveryCost);
        }

        public IEnumerable<Restaurant> GetAllByUser(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (base.GetAll(userId, noTracking));
            }
            var restaurantIds = (ServiceUnitOfWork.RestaurantUsers.GetAll(userId, noTracking)).Select(e => e.RestaurantId);
            return (base.GetAll(userId, noTracking)).Where(e => restaurantIds.Contains(e.Id));
        }

        public async Task<IEnumerable<Restaurant>> GetAllByUserAsync(object? userId, bool noTracking = true) {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => restaurantIds.Contains(e.Id));
        }
    }
}