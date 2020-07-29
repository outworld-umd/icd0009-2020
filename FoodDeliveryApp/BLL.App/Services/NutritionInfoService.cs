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
    public class NutritionInfoService : BaseEntityService<IAppUnitOfWork, INutritionInfoRepository, INutritionInfoServiceMapper, DAL.App.DTO.NutritionInfo, NutritionInfo>, INutritionInfoService
    {
        public NutritionInfoService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.NutritionInfos, new NutritionInfoServiceMapper())
        {
        }

        public async Task<IEnumerable<NutritionInfo>> GetAllByItemAsync(Guid itemId, object? userId = null, bool noTracking = true) {
            return (await ServiceRepository.GetAllAsync(userId, noTracking)).Where(n => n.ItemId.Equals(itemId))
                .Select(e => BLLMapper.Map(e));
        }
    }
}