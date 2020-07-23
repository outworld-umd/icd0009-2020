using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<IOrderRepository, IAppUnitOfWork, DAL.App.DTO.Order, Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.Order, Order>(), unitOfWork.Orders)
        {
        }
    }
}