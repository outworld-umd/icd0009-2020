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
    public class ItemChoiceService : BaseEntityService<IAppUnitOfWork, IItemChoiceRepository, IItemChoiceServiceMapper, DAL.App.DTO.ItemChoice, ItemChoice>, IItemChoiceService
    {
        public ItemChoiceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemChoices, new ItemChoiceServiceMapper())
        {
        }

        public IEnumerable<ItemChoice> GetAllByUser(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (base.GetAll(userId, noTracking));
            }
            var restaurantIds = (ServiceUnitOfWork.RestaurantUsers.GetAll(userId, noTracking)).Select(e => e.RestaurantId);
            return (base.GetAll(userId, noTracking)).Where(e => restaurantIds.Contains(e.ItemOption!.Item!.RestaurantId!.Value));
            
        }

        public async Task<IEnumerable<ItemChoice>> GetAllByUserAsync(object? userId, bool noTracking = true)
        {
            if (userId == null)
            {
                return (await base.GetAllAsync(userId, noTracking));
            }
            var restaurantIds = (await ServiceUnitOfWork.RestaurantUsers.GetAllAsync(userId, noTracking)).Select(e => e.RestaurantId);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => restaurantIds.Contains(e.ItemOption!.Item!.RestaurantId!.Value));
            
        }
    }
}