using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, DAL.App.DTO.Order, Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Orders, new OrderServiceMapper())
        {
        }

        public async Task<IEnumerable<Order>> GetAllByRestaurantAsync(object? userId = null, bool noTracking = true)
        {
            var orders = (await ServiceRepository.GetAllAsync(userId, noTracking)).Where(o =>
                o.Restaurant!.RestaurantUsers.Select(ru => ru.AppUserId).Equals(userId));

            var result = orders.Select(e => BLLMapper.Map(e));
            return result;      
        }
    }
}