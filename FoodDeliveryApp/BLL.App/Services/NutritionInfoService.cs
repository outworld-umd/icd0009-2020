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
    }
}