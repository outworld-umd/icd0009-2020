using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface IRestaurantCategoryService : IBaseEntityService<RestaurantCategory>
    {
        public Task<IEnumerable<RestaurantCategory>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}