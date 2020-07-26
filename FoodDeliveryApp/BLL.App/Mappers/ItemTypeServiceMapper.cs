using AutoMapper;
using AutoMapper.Configuration;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemTypeServiceMapper: BaseBLLMapper<DALAppDTO.ItemType, BLLAppDTO.ItemType>, IItemTypeServiceMapper
    {
        public ItemTypeServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemType, DALAppDTO.ItemType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemType, BLLAppDTO.ItemType>();

            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}