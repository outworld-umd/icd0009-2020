using System;
using System.Linq;
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
            var restaurant = Mapper.Map<Restaurant>(inObject);
            return restaurant;
        }
        
        public RestaurantView MapRestaurantView(BLL.App.DTO.Restaurant inObject)
        {
            var restaurant = Mapper.Map<RestaurantView>(inObject);
            restaurant.Categories = inObject.RestaurantCategories.Select(rc => rc.Category!.Name).ToList();
            restaurant.IsOpen = true;
            return restaurant;
        }
    }
}