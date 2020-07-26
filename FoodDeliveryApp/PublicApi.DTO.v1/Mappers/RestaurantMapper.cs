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
            restaurant.Categories = inObject.RestaurantCategories.Select(rc => new CategoryView {
                Id = rc.Category!.Id,
                Name = rc.Category!.Name,
                RestaurantCategoryId = rc.Id
            }).ToList();
            restaurant.IsOpen = inObject.WorkingHourses.Select(wh => wh.WeekDay).Single() == DateTime.Today.DayOfWeek;
            return restaurant;
        }
    }
}