using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderRowServiceMapper: BaseBLLMapper<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>, IOrderRowServiceMapper
    {
        public OrderRowServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderRow, DALAppDTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Order, DALAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderItemChoice, DALAppDTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderItemChoice, BLLAppDTO.OrderItemChoice>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.OrderRow MapOrderRow(DALAppDTO.OrderRow inObject)
        {
            var orderRow = Mapper.Map<BLLAppDTO.OrderRow>(inObject);
            if (orderRow.Name!.Equals(null)) {
                orderRow.Name = inObject.Item!.Name;
            }
            return orderRow;
        }
    }
}