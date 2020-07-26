using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantServiceMapper: BaseBLLMapper<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>, IRestaurantServiceMapper
    {
        public RestaurantServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RestaurantCategory, DALAppDTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantCategory, BLLAppDTO.RestaurantCategory>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Category, DALAppDTO.Category>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Category, BLLAppDTO.Category>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemType, DALAppDTO.ItemType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemType, BLLAppDTO.ItemType>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.WorkingHours, DALAppDTO.WorkingHours>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.WorkingHours, BLLAppDTO.WorkingHours>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}