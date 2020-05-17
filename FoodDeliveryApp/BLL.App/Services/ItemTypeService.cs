using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemTypeService : BaseEntityService<IItemTypeRepository, IAppUnitOfWork, DAL.App.DTO.ItemType, ItemType>, IItemTypeService
    {
        public ItemTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ItemType, ItemType>(), unitOfWork.ItemTypes)
        {
        }
    }
}