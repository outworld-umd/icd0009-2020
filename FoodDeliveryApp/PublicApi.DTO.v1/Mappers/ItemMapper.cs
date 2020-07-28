using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemMapper: BaseAPIMapper<BLL.App.DTO.Item, Item>
    {
        public ItemMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, Item>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Item, ItemView>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Item MapItem(BLL.App.DTO.Item inObject)
        {
            var item = Mapper.Map<Item>(inObject);
            item.NutritionInfos = inObject.NutritionInfos
                .Select(ni => new NutritionInfoMapper().MapNutritionInfo(ni))
                .ToList();
            item.ItemOptions = inObject.ItemOptions
                .Select(io => new ItemOptionMapper().MapItemOption(io))
                .ToList();
            return item;
        }
        
        public ItemView MapItemView(BLL.App.DTO.Item inObject)
        {
            return Mapper.Map<ItemView>(inObject);
        }
    }
}