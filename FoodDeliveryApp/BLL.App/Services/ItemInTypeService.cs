using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemInTypeService : BaseEntityService<IItemInTypeRepository, IAppUnitOfWork, DAL.App.DTO.ItemInType, ItemInType>, IItemInTypeService
    {
        public ItemInTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ItemInType, ItemInType>(), unitOfWork.ItemInTypes)
        {
        }
    }
}