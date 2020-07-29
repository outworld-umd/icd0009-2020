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
    public class RestaurantService : BaseEntityService<IAppUnitOfWork, IRestaurantRepository, IRestaurantServiceMapper, DAL.App.DTO.Restaurant, Restaurant>, IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Restaurants, new RestaurantServiceMapper())
        {
        }

        public async Task<IEnumerable<Restaurant>> GetAllSortedByDeliveryAsync(object? userId = null, bool noTracking = true) {
            return (await base.GetAllAsync(userId, noTracking)).OrderBy(e => e.DeliveryCost);
        }
    }
}