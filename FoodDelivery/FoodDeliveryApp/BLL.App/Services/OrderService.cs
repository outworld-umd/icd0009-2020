using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.anguzo.BLL.Base.Services;using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, DAL.App.DTO.Order, Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Orders, new OrderServiceMapper())
        {
        }

        public async Task<IEnumerable<Order>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true) {
            var dalEntities = await ServiceRepository.GetAllByRestaurantAsync(restaurantId, userId, noTracking);
            return dalEntities.Select(e => BLLMapper.Map(e));
        }

        public async Task<IEnumerable<Order>> GetAllByUserAsync(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(null, noTracking)).Where(e => restaurantIds.Contains(e.RestaurantId!.Value));        }
    }
}