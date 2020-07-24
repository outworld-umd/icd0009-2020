using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class ItemChoiceMapper: BaseAPIMapper<BLL.App.DTO.ItemChoice, ItemChoice>
    {
        public ItemChoiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemChoice, ItemChoice>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ItemChoice MapItemChoice(BLL.App.DTO.ItemChoice inObject)
        {
            return Mapper.Map<ItemChoice>(inObject);
        }
    }
}