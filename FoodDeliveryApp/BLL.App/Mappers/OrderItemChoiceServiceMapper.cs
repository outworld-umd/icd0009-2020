using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderItemChoiceServiceMapper: BaseBLLMapper<DALAppDTO.OrderItemChoice, BLLAppDTO.OrderItemChoice>, IOrderItemChoiceServiceMapper
    {
        public OrderItemChoiceServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderItemChoice, DALAppDTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderItemChoice, BLLAppDTO.OrderItemChoice>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderRow, DALAppDTO.OrderRow>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemChoice, DALAppDTO.ItemChoice>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}