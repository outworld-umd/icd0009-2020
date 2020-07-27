using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderRowService : BaseEntityService<IAppUnitOfWork, IOrderRowRepository, IOrderRowServiceMapper, DAL.App.DTO.OrderRow, OrderRow>, IOrderRowService
    {
        public OrderRowService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.OrderRows, new OrderRowServiceMapper())
        {
        }

        public new OrderRow Add(OrderRow entity) {
            if (entity.ItemId != null) 
            {
                entity.Name ??= ServiceUnitOfWork.Items.FirstOrDefaultAsync(entity.ItemId.Value).Result.Name;
            }
            entity.Name ??= "";
            return base.Add(entity);
        }
    }
}