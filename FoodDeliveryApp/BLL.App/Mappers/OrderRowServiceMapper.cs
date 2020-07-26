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
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderRow, DALAppDTO.OrderRow>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Order, DALAppDTO.Order>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderItemChoice, DALAppDTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderItemChoice, BLLAppDTO.OrderItemChoice>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}