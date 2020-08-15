﻿using AutoMapper;
 using PublicApi.DTO.v1.Mappers.Base;
 using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemInTypeMapper: APIMapper<BLL.App.DTO.ItemInType, ItemInType>
    {

        public ItemInType MapItemInType(BLL.App.DTO.ItemInType inObject)
        {
            return Mapper.Map<ItemInType>(inObject);
        }
    }
}