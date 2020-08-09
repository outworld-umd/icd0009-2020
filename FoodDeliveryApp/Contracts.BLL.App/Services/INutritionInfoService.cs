using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface INutritionInfoService : IBaseEntityService<NutritionInfo>
    {

        public Task<IEnumerable<NutritionInfo>> GetAllByItemAsync(Guid itemId, object? userId = null, bool noTracking = true);
        
        public IEnumerable<NutritionInfo> GetAllByUser(object? userId, bool noTracking = true);

        public Task<IEnumerable<NutritionInfo>> GetAllByUserAsync(object? userId, bool noTracking = true);
    }
}