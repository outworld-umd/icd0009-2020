using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class ItemTypeMapper: BaseAPIMapper<BLL.App.DTO.ItemType, ItemType>
    {
        public ItemTypeMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemType, ItemType>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ItemType MapItemType(BLL.App.DTO.ItemType inObject)
        {
            return Mapper.Map<ItemType>(inObject);
        }
    }
}