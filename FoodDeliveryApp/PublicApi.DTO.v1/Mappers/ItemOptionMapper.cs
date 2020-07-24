using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class ItemOptionMapper: BaseAPIMapper<BLL.App.DTO.ItemOption, ItemOption>
    {
        public ItemOptionMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemOption, ItemOption>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ItemOption MapItemOption(BLL.App.DTO.ItemOption inObject)
        {
            return Mapper.Map<ItemOption>(inObject);
        }
    }
}