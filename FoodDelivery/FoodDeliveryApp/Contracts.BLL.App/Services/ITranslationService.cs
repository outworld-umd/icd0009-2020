using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.anguzo.Contracts.BLL.Base.Services;namespace Contracts.BLL.App.Services
{
    public interface ITranslationService : IBaseEntityService<Translation>
    {
        public Task<IEnumerable<Translation>> GetAllAsyncWithoutDefault(object? userId = null, bool noTracking = true);
        public Task<IEnumerable<Translation>> GetAllAsyncDefault(object? userId = null, bool noTracking = true);
        public IEnumerable<Translation> GetAllDefault(object? userId = null, bool noTracking = true);

    }
}