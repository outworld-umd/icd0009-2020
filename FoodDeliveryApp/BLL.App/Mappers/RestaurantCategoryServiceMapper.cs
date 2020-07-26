using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantCategoryServiceMapper: BaseBLLMapper<DALAppDTO.RestaurantCategory, BLLAppDTO.RestaurantCategory>, IRestaurantCategoryServiceMapper
    {
        public RestaurantCategoryServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RestaurantCategory, DALAppDTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantCategory, BLLAppDTO.RestaurantCategory>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Category, DALAppDTO.Category>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Category, BLLAppDTO.Category>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}