using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IRestaurantService : IBaseEntityService<Restaurant>
    {

        public Task<IEnumerable<Restaurant>> GetAllSortedByDeliveryAsync(object? userId = null, bool noTracking = true);

        public IEnumerable<Restaurant> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<Restaurant>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}