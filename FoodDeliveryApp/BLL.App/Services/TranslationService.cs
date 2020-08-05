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
    }
}