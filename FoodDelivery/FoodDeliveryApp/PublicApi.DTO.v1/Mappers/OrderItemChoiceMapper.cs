using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class OrderItemChoiceMapper: APIMapper<BLL.App.DTO.OrderItemChoice, OrderItemChoice>
    {

        public OrderItemChoice MapOrderItemChoice(BLL.App.DTO.OrderItemChoice inObject)
        {
            var orderItemChoice = Mapper.Map<OrderItemChoice>(inObject);
            return orderItemChoice;
        }
    }
}