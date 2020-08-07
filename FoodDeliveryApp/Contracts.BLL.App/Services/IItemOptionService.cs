using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IItemOptionService : IBaseEntityService<ItemOption>
    {
        public Task<IEnumerable<ItemOption>> GetAllByRestaurantAsync(object? restaurantId, object? userId = null,
            bool noTracking = true);
    }
}