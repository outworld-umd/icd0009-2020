using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

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
            return Mapper.Map<OrderRow>(inObject);
        }
    }
}