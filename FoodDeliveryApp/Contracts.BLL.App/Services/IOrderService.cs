using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>
    {
        public Task<IEnumerable<Order>> GetAllByRestaurantIdAsync(ClaimsPrincipal user, bool noTracking = true);

    }
}