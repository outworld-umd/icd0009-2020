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
    public class TranslationService :
        BaseEntityService<IAppUnitOfWork, ITranslationRepository, ITranslationServiceMapper,
            DAL.App.DTO.Translation, BLL.App.DTO.Translation>, ITranslationService
    {
        public TranslationService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Translations, new TranslationServiceMapper())
        {
        }
        
        public async Task<IEnumerable<Translation>> GetAllAsyncWithoutDefault(object? userId = null, bool noTracking = true)
        {
            return (await base.GetAllAsync(userId, noTracking)).Where(e => e.Culture != "en-US");
        }
        
        public async Task<IEnumerable<Translation>> GetAllAsyncDefault(object? userId = null, bool noTracking = true)
        {
            var categoryNames = (await ServiceUnitOfWork.Categories.GetAllAsync(userId, noTracking)).Select(c => c.Name);
            return (await base.GetAllAsync(userId, noTracking)).Where(e => categoryNames.Contains(e.Value));
        }
        
        public IEnumerable<Translation> GetAllDefault(object? userId = null, bool noTracking = true)
        {
            var categoryNames = ServiceUnitOfWork.Categories.GetAll(userId, noTracking).Select(c => c.Name);
            return base.GetAll(userId, noTracking).Where(e => categoryNames.Contains(e.Value));
        }
    }
}