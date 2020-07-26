using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemInTypeServiceMapper: BaseBLLMapper<DALAppDTO.ItemInType, BLLAppDTO.ItemInType>, IItemInTypeServiceMapper
    {
        public ItemInTypeServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemInType, DALAppDTO.ItemInType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemInType, BLLAppDTO.ItemInType>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemType, BLLAppDTO.ItemType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemType, DALAppDTO.ItemType>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}