using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemServiceMapper: BaseBLLMapper<DALAppDTO.Item, BLLAppDTO.Item>, IItemServiceMapper
    {
        public ItemServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.NutritionInfo, BLLAppDTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.NutritionInfo, DALAppDTO.NutritionInfo>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}