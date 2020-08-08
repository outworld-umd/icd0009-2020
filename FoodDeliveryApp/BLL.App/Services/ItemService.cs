using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ItemService : BaseEntityService<IAppUnitOfWork, IItemRepository, IItemServiceMapper, DAL.App.DTO.Item, Item>, IItemService
    {
        public ItemService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Items, new ItemServiceMapper())
        {
        }
        
        public async Task<IEnumerable<Item>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true)
        {
            var dalEntities = await ServiceRepository.GetAllByRestaurantAsync(restaurantId, userId, noTracking);
            return dalEntities.Select(e => BLLMapper.Map(e)).OrderBy(e => e.Name);
        }

        public IEnumerable<Item> GetAllByUser(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (base.GetAll(userId, noTracking));
            }
            var restaurantIds = (ServiceUnitOfWork.RestaurantUsers.GetAll(userId, noTracking)).Select(e => e.RestaurantId);
            return (base.GetAll(userId, noTracking)).Where(e => restaurantIds.Contains(e.RestaurantId!.Value));
        }

        public async Task<IEnumerable<Item>> GetAllByUserAsync(object? userId, bool noTracking = true) 
        {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => restaurantIds.Contains(e.RestaurantId!.Value));
        }
    }
}