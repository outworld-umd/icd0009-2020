using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderMapper: BaseAPIMapper<BLL.App.DTO.Order, Order>
    {
        public OrderMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Order, Order>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Order, OrderView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Order MapOrder(BLL.App.DTO.Order inObject)
        {
            var order = Mapper.Map<Order>(inObject);
            return order;
        }
        
        public OrderView MapOrderView(BLL.App.DTO.Order inObject)
        {
            var order = Mapper.Map<Order>(inObject);
            return order;
            
        }
    }
}