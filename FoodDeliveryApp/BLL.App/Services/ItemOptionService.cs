using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemOptionService : BaseEntityService<IItemOptionRepository, IAppUnitOfWork, DAL.App.DTO.ItemOption, ItemOption>, IItemOptionService
    {
        public ItemOptionService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.ItemOption, ItemOption>(), unitOfWork.ItemOptions)
        {
        }
    }
}