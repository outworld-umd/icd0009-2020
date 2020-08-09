using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {

    public interface IItemTypeRepository : IBaseRepository<ItemType> {

        public Task<IEnumerable<ItemType>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true);
    }

}