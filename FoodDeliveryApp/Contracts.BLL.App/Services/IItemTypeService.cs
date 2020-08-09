using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IItemTypeService : IBaseEntityService<ItemType>
    {
        public Task<IEnumerable<ItemType>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true);
        public IEnumerable<ItemType> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<ItemType>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}