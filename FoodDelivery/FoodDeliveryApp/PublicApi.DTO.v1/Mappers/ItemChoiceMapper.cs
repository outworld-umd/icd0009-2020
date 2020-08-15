using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemChoiceMapper: APIMapper<BLL.App.DTO.ItemChoice, ItemChoice>
    {

        public ItemChoice MapItemChoice(BLL.App.DTO.ItemChoice inObject)
        {
            return Mapper.Map<ItemChoice>(inObject);
        }
    }
}