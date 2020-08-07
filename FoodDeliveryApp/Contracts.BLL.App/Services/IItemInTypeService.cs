using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IItemInTypeService : IBaseEntityService<ItemInType>
    {
        public Task<IEnumerable<ItemInType>> GetAllByRestaurantAsync(object? restaurantId, object? userId = null,
            bool noTracking = true);
    }
}