using System.Linq;
using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemOptionMapper: APIMapper<BLL.App.DTO.ItemOption, ItemOption>
    {

        public ItemOption MapItemOption(BLL.App.DTO.ItemOption inObject)
        {
            var itemOption = Mapper.Map<ItemOption>(inObject);
            return itemOption;
        }
    }
}