using System;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemTypeMapper: APIMapper<BLL.App.DTO.ItemType, ItemType>
    {

        public ItemType MapItemType(BLL.App.DTO.ItemType inObject)
        {
            var itemType = Mapper.Map<ItemType>(inObject);
            itemType.Items = inObject.ItemInTypes
                .Select(ic => new ItemView
                {
                    ItemInTypeId = ic.Id,
                    Name = ic.Item!.Name,
                    PictureLink = ic.Item!.PictureLink,
                    Price = ic.Item!.Price,
                    Id = ic.ItemId
                })
                .ToList();
            return itemType;
        }
    }
}