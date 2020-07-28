using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


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
            var itemOption = Mapper.Map<ItemOption>(inObject);
            itemOption.ItemChoices = inObject.ItemChoices
                .Select(ic => new ItemChoiceMapper().MapItemChoice(ic))
                .ToList();
            return itemOption;
        }
    }
}