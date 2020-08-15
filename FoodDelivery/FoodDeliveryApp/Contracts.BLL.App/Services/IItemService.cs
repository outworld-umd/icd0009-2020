using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IItemService : IBaseEntityService<Item>
    {
        public Task<IEnumerable<Item>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null,
            bool noTracking = true);
        public IEnumerable<Item> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<Item>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}