using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class NutritionInfoService : BaseEntityService<INutritionInfoRepository, IAppUnitOfWork, DAL.App.DTO.NutritionInfo, NutritionInfo>, INutritionInfoService
    {
        public NutritionInfoService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.NutritionInfo, NutritionInfo>(), unitOfWork.NutritionInfos)
        {
        }
    }
}