using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderRowService : BaseEntityService<IOrderRowRepository, IAppUnitOfWork, DAL.App.DTO.OrderRow, OrderRow>, IOrderRowService
    {
        public OrderRowService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BLLMapper<DAL.App.DTO.OrderRow, OrderRow>(), unitOfWork.OrderRows)
        {
        }
    }
}