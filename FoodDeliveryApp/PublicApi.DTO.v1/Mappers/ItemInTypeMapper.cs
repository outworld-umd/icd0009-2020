﻿using AutoMapper;
 using PublicApi.DTO.v1.Mappers.Base;
 using BLLAppDTO=BLL.App.DTO;


namespace PublicApi.DTO.v1.Mappers
{
    public class ItemInTypeMapper: BaseAPIMapper<BLL.App.DTO.ItemInType, ItemInType>
    {
        public ItemInTypeMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemInType, ItemInType>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ItemInType MapItemInType(BLL.App.DTO.ItemInType inObject)
        {
            return Mapper.Map<ItemInType>(inObject);
        }
    }
}