using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class LangStrService :
        BaseEntityService<IAppUnitOfWork, ILangStrRepository, ILangStrServiceMapper,
            DAL.App.DTO.LangStr, BLL.App.DTO.LangStr>, ILangStrService
    {
        public LangStrService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.LangStrings, new LangStrServiceMapper())
        {
        }
    }
}