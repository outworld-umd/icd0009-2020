using BLL.App.Mappers;
using ee.itcollege.anguzo.BLL.Base.Services;using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class LangStringService :
        BaseEntityService<IAppUnitOfWork, ILangStringRepository, ILangStringServiceMapper,
            DAL.App.DTO.LangString, BLL.App.DTO.LangString>, ILangStringService
    {
        public LangStringService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.LangStrings, new LangStringServiceMapper())
        {
        }
    }
}