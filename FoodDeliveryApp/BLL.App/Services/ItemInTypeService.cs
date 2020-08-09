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
    public class ItemInTypeService : BaseEntityService<IAppUnitOfWork, IItemInTypeRepository, IItemInTypeServiceMapper, DAL.App.DTO.ItemInType, ItemInType>, IItemInTypeService
    {
        public ItemInTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemInTypes, new ItemInTypeServiceMapper())
        {
        }

        public IEnumerable<ItemInType> GetAllByUser(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (base.GetAll(userId, noTracking));
            }
            var restaurantIds = (ServiceUnitOfWork.RestaurantUsers.GetAll(userId, noTracking)).Select(e => e.RestaurantId);
            return (base.GetAll(userId, noTracking)).Where(e => restaurantIds.Contains(e.Item!.RestaurantId!.Value));
            
        }

        public async Task<IEnumerable<ItemInType>> GetAllByUserAsync(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => restaurantIds.Contains(e.Item!.RestaurantId!.Value));
            
        }
    }
}