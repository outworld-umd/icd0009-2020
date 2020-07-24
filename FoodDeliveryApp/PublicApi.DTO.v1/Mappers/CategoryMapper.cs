using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class CategoryMapper: BaseAPIMapper<BLL.App.DTO.Category, Category>
    {
        public CategoryMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, Category>();
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