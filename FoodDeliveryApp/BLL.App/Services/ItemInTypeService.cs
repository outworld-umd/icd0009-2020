using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemInTypeService : BaseEntityService<IAppUnitOfWork, IItemInTypeRepository, IItemInTypeServiceMapper, DAL.App.DTO.ItemInType, ItemInType>, IItemInTypeService
    {
        public ItemInTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemInTypes, new ItemInTypeServiceMapper())
        {
        }
    }
}