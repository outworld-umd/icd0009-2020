using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class CategoryServiceMapper: BaseBLLMapper<DALAppDTO.Category, BLLAppDTO.Category>, ICategoryServiceMapper
    {
        public CategoryServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.RestaurantCategory, DALAppDTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantCategory, BLLAppDTO.RestaurantCategory>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Category, DALAppDTO.Category>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Category, BLLAppDTO.Category>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}