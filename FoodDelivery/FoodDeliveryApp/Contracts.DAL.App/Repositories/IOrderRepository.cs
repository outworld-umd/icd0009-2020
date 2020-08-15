using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {

    public interface IOrderRepository : IBaseRepository<Order>
    {
        public Task<IEnumerable<Order>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true);
    }

}