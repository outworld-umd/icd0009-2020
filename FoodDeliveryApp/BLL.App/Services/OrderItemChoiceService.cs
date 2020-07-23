using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderItemChoiceService : BaseEntityService<IAppUnitOfWork, IOrderItemChoiceRepository, IOrderItemChoiceServiceMapper, DAL.App.DTO.OrderItemChoice, OrderItemChoice>, IOrderItemChoiceService
    {
        public OrderItemChoiceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.OrderItemChoices, new OrderItemChoiceServiceMapper())
        {
        }
    }
}