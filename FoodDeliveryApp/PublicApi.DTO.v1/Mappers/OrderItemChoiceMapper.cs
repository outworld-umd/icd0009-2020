using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderItemChoiceMapper: BaseAPIMapper<BLL.App.DTO.OrderItemChoice, OrderItemChoice>
    {
        public OrderItemChoiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderItemChoice, OrderItemChoice>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public OrderItemChoice MapOrderItemChoice(BLL.App.DTO.OrderItemChoice inObject)
        {
            return Mapper.Map<OrderItemChoice>(inObject);
        }
    }
}