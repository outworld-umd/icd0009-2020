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

        public async Task<IEnumerable<Order>> GetAllByRestaurantIdAsync(ClaimsPrincipal user, bool noTracking = true)
        {
            IEnumerable<DAL.App.DTO.Order> orders = null;
            if (user.IsInRole("Admin"))
            {
                orders = await ServiceRepository.GetAllAsync(noTracking);
            }
            else if (user.IsInRole("Restaurant"))
            {
                orders = orders.Where(o =>
                    o.Restaurant.RestaurantUsers.Select(ru => ru.AppUserId).Equals(user.UserGuidId()));
            }
            else if (user.IsInRole("Customer"))
            {
                orders = await ServiceRepository.GetAllAsync(user.UserGuidId(), noTracking);
            }
            var result = orders.Select(e => BLLMapper.Map(e));
            return result;      
        }
    }
}