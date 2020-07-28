using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class NutritionInfoMapper: APIMapper<BLL.App.DTO.NutritionInfo, NutritionInfo>
    {

        public NutritionInfo MapNutritionInfo(BLL.App.DTO.NutritionInfo inObject)
        {
            return Mapper.Map<NutritionInfo>(inObject);
        }
    }
}