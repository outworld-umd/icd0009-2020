using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IItemChoiceService : IBaseEntityService<ItemChoice>
    {
        public Task<IEnumerable<ItemChoice>> GetAllByRestaurantAsync(object? restaurantId, object? userId = null,
            bool noTracking = true);
    }
}