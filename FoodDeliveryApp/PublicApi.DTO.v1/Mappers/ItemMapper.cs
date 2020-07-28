using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using Newtonsoft.Json;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemMapper: APIMapper<BLL.App.DTO.Item, Item>
    {

        public Item MapItem(BLL.App.DTO.Item inObject)
        {
            var item = Mapper.Map<Item>(inObject);
            return item;
        }
        
        public ItemView MapItemView(BLL.App.DTO.Item inObject)
        {
            return Mapper.Map<ItemView>(inObject);
        }
    }
}