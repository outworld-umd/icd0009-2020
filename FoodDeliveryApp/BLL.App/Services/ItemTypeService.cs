using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemTypeService : BaseEntityService<IAppUnitOfWork, IItemTypeRepository, IItemTypeServiceMapper, DAL.App.DTO.ItemType, ItemType>, IItemTypeService
    {
        public ItemTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemTypes, new ItemTypeServiceMapper())
        {
        }
    }
}