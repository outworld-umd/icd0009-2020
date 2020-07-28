using System;
using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class RestaurantMapper: APIMapper<BLL.App.DTO.Restaurant, Restaurant>
    {

        public Restaurant MapRestaurant(BLL.App.DTO.Restaurant inObject)
        {
            var restaurant = Mapper.Map<Restaurant>(inObject);
            restaurant.WorkingHourses = inObject.WorkingHourses.Select(wh => new WorkingHoursMapper().MapWorkingHours(wh)).ToList();
            restaurant.ItemTypes = inObject.ItemTypes.Select(it => new ItemTypeMapper().MapItemType(it)).ToList();
            restaurant.Categories = inObject.RestaurantCategories.Select(rc => new CategoryView {
                Id = rc.Category!.Id,
                Name = rc.Category!.Name,
                RestaurantCategoryId = rc.Id
            }).ToList();
            // restaurant.IsOpen = IsRestaurantOpen(inObject);
            restaurant.IsOpen = true;
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
            // restaurant.IsOpen = IsRestaurantOpen(inObject);
            restaurant.IsOpen = true;
            return restaurant;
        }

        private static bool IsRestaurantOpen(BLL.App.DTO.Restaurant inObject)
        {
            var start = inObject.WorkingHourses.Single(wh => wh.WeekDay.Equals(DateTime.Today.DayOfWeek)).OpeningTime;
            var end = inObject.WorkingHourses.Single(wh => wh.WeekDay.Equals(DateTime.Today.DayOfWeek)).ClosingTime;
            var now = DateTime.Now;
            return (now > start) && (now < end);
        }
    }
}