using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemChoiceService : BaseEntityService<IItemChoiceRepository, IAppUnitOfWork, DAL.App.DTO.ItemChoice, ItemChoice>, IItemChoiceService
    {
        public ItemChoiceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ItemChoice, ItemChoice>(), unitOfWork.ItemChoices)
        {
        }
    }
}