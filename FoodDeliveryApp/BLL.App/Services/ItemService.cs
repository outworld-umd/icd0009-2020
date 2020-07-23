using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemService : BaseEntityService<IAppUnitOfWork, IItemRepository, IItemServiceMapper, DAL.App.DTO.Item, Item>, IItemService
    {
        public ItemService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Items, new ItemServiceMapper())
        {
        }
    }
}