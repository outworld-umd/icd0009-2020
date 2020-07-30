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
        public Task<IEnumerable<Restaurant>> GetAllByUser(Guid userId, bool noTracking = true);
    }
}