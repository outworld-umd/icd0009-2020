using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemService : BaseEntityService<IItemRepository, IAppUnitOfWork, DAL.App.DTO.Item, Item>, IItemService
    {
        public ItemService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Item, Item>(), unitOfWork.Items)
        {
        }
    }
}