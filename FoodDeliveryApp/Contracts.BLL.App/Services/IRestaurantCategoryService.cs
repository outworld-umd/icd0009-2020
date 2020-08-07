using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface IRestaurantCategoryService : IBaseEntityService<RestaurantCategory>
    {
        public Task<IEnumerable<RestaurantCategory>> GetAllByUser(object? userId, bool noTracking = true);
    }
}