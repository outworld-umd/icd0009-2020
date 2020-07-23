using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderItemChoiceService : BaseEntityService<IOrderItemChoiceRepository, IAppUnitOfWork, DAL.App.DTO.OrderItemChoice, OrderItemChoice>, IOrderItemChoiceService
    {
        public OrderItemChoiceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.OrderItemChoice, OrderItemChoice>(), unitOfWork.OrderItemChoices)
        {
        }
    }
}