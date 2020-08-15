using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class OrderMapper: APIMapper<BLL.App.DTO.Order, Order>
    {
        public Order MapOrder(BLL.App.DTO.Order inObject)
        {
            var order = Mapper.Map<Order>(inObject);
            return order;
        }
        
        public OrderView MapOrderView(BLL.App.DTO.Order inObject)
        {
            return Mapper.Map<OrderView>(inObject);
        }
    }
}