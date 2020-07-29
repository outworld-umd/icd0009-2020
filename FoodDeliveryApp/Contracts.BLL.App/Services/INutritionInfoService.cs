using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface INutritionInfoService : IBaseEntityService<NutritionInfo>
    {

        public Task<IEnumerable<NutritionInfo>> GetAllByItemAsync(Guid itemId, object? userId = null, bool noTracking = true);
    }
}