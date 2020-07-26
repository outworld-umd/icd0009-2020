using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemOptionServiceMapper: BaseBLLMapper<DALAppDTO.ItemOption, BLLAppDTO.ItemOption>, IItemOptionServiceMapper
    {
        public ItemOptionServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemOption, DALAppDTO.ItemOption>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemOption, BLLAppDTO.ItemOption>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemChoice, DALAppDTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}