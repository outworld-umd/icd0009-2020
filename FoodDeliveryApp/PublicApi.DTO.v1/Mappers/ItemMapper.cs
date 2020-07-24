using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class ItemMapper: BaseAPIMapper<BLL.App.DTO.Item, Item>
    {
        public ItemMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, Item>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, ItemView>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Item MapItem(BLL.App.DTO.Item inObject)
        {
            return Mapper.Map<Item>(inObject);
        }
        
        public ItemView MapItemView(BLL.App.DTO.Item inObject)
        {
            return Mapper.Map<ItemView>(inObject);
        }
    }
}