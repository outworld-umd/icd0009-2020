using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class CategoryMapper: BaseAPIMapper<BLL.App.DTO.Category, Category>
    {
        public CategoryMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Category, Category>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, Restaurant>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Category MapCategory(BLL.App.DTO.Category inObject)
        {
            var category = Mapper.Map<Category>(inObject);
            category.Restaurants = inObject.RestaurantCategories
                .Select(rc => new RestaurantMapper().MapRestaurantView(rc.Restaurant!))
                .ToList();
            return category;
        }
    }
}