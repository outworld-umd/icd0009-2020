using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class NutritionInfoServiceMapper: BaseBLLMapper<DALAppDTO.NutritionInfo, BLLAppDTO.NutritionInfo>, INutritionInfoServiceMapper
    {
        public NutritionInfoServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.NutritionInfo, DALAppDTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.NutritionInfo, BLLAppDTO.NutritionInfo>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, DALAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Item, BLLAppDTO.Item>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}