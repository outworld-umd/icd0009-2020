using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ITranslationService : IBaseEntityService<Translation>
    {
        public Task<IEnumerable<Translation>> GetAllAsyncWithoutDefault(object? userId = null, bool noTracking = true);
    }
}