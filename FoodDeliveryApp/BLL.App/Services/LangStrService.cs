using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class LangStrService :
        BaseEntityService<IAppUnitOfWork, ILangStrRepository, ILangStringServiceMapper,
            DAL.App.DTO.LangString, BLL.App.DTO.LangString>, ILangStrService
    {
        public LangStrService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.LangStrings, new LangStringServiceMapper())
        {
        }
    }
}