﻿using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;

namespace PublicApi.DTO.v1.Mappers
{
    public class AddressMapper: BaseAPIMapper<BLL.App.DTO.Address, Address>
    {
        public AddressMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Address, Address>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Address MapAddress(BLL.App.DTO.Address inObject)
        {
            return Mapper.Map<Address>(inObject);
        }
    }
}