using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class NutritionInfoMapper: BaseAPIMapper<BLL.App.DTO.NutritionInfo, NutritionInfo>
    {
        public NutritionInfoMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.NutritionInfo, NutritionInfo>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public NutritionInfo MapNutritionInfo(BLL.App.DTO.NutritionInfo inObject)
        {
            return Mapper.Map<NutritionInfo>(inObject);
        }
    }
}