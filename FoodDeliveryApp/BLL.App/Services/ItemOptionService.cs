using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemOptionService : BaseEntityService<IAppUnitOfWork, IItemOptionRepository, IItemOptionServiceMapper, DAL.App.DTO.ItemOption, ItemOption>, IItemOptionService
    {
        public ItemOptionService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemOptions, new ItemOptionServiceMapper())
        {
        }

        public Task<IEnumerable<ItemOption>> GetAllByRestaurantAsync(object? restaurantId, object? userId = null, bool noTracking = true)
        {
            throw new System.NotImplementedException();
        }
    }
}