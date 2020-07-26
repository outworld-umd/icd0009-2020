using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


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
            var itemType = Mapper.Map<ItemType>(inObject);
            itemType.Items = inObject.ItemInTypes
                .Select(ic => new ItemMapper().MapItemView(ic.Item!))
                .ToList();
            return itemType;
        }
    }
}