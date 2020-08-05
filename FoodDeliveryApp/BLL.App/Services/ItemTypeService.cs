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
    public class ItemTypeService : BaseEntityService<IAppUnitOfWork, IItemTypeRepository, IItemTypeServiceMapper, DAL.App.DTO.ItemType, ItemType>, IItemTypeService
    {
        public ItemTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemTypes, new ItemTypeServiceMapper())
        {
        }

        public async Task<IEnumerable<ItemType>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true) {
            var dalEntities = await ServiceRepository.GetAllByRestaurantAsync(restaurantId, userId, noTracking);
            return dalEntities.Select(e => BLLMapper.Map(e)).OrderBy(e => e.IsSpecial).ThenBy(e => e.Name);
        }
    }
}