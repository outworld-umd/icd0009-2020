using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemChoiceService : BaseEntityService<IAppUnitOfWork, IItemChoiceRepository, IItemChoiceServiceMapper, DAL.App.DTO.ItemChoice, ItemChoice>, IItemChoiceService
    {
        public ItemChoiceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ItemChoices, new ItemChoiceServiceMapper())
        {
        }
    }
}