using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {

    public interface IItemRepository : IBaseRepository<Item> 
    {
        public Task<IEnumerable<Item>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true);
    }

}