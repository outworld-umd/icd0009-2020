using System;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.anguzo.BLL.Base.Services;using Contracts.BLL.App.Mappers;
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

        public new OrderItemChoice Add(OrderItemChoice entity)
        {
            entity.Name = (ServiceUnitOfWork.ItemChoices.FirstOrDefaultAsync(entity.ItemChoiceId!.Value).Result).Name;
            return base.Add(entity);
        }
    }
}