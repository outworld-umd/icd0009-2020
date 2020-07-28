using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class OrderRowMapper: BaseAPIMapper<BLL.App.DTO.OrderRow, OrderRow>
    {
        public OrderRowMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderRow, OrderRow>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public OrderRow MapOrderRow(BLL.App.DTO.OrderRow inObject)
        {
            var orderRow = Mapper.Map<OrderRow>(inObject);
            orderRow.OrderItemChoices = inObject.OrderItemChoices.Select(oic => new OrderItemChoiceMapper().MapOrderItemChoice(oic))
                .ToList();
            return orderRow;
        }
    }
}