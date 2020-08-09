using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order> {
        public Task<IEnumerable<Order>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true);
        public Task<IEnumerable<Order>> GetAllByUserAsync(object? userId, bool noTracking = true);

    }
}