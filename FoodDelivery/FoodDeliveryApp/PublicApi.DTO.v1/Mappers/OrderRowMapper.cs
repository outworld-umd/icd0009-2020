using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class OrderRowMapper: APIMapper<BLL.App.DTO.OrderRow, OrderRow>
    {

        public OrderRow MapOrderRow(BLL.App.DTO.OrderRow inObject)
        {
            var orderRow = Mapper.Map<OrderRow>(inObject);
            orderRow.Choices = inObject.OrderItemChoices.Select(oic => new OrderItemChoiceMapper().MapOrderItemChoice(oic))
                .ToList();
            return orderRow;
        }
    }
}