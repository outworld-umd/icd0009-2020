using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class RestaurantMapper: BaseAPIMapper<BLL.App.DTO.Restaurant, Restaurant>
    {
        public RestaurantMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, Restaurant>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, RestaurantView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Restaurant MapRestaurant(BLL.App.DTO.Restaurant inObject)
        {
            return Mapper.Map<Restaurant>(inObject);
        }
        
        public RestaurantView MapRestaurantView(BLL.App.DTO.Restaurant inObject)
        {
            return Mapper.Map<RestaurantView>(inObject);
        }
    }
}