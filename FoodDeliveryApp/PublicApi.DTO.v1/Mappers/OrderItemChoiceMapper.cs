using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


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
            var orderItemChoice = Mapper.Map<OrderItemChoice>(inObject);
            orderItemChoice.ItemChoiceName = inObject.ItemChoice!.Name;
            return orderItemChoice;
        }
    }
}