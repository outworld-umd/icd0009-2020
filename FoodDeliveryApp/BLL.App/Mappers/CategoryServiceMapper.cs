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
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantCategory, BLLAppDTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Category, BLLAppDTO.Category>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RestaurantCategory, DALAppDTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Category, DALAppDTO.Category>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}