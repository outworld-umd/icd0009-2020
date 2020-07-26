﻿using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
 using BLLAppDTO=BLL.App.DTO;

 namespace PublicApi.DTO.v1.Mappers
{
    public class AddressMapper: BaseAPIMapper<BLL.App.DTO.Address, Address>
    {
        public AddressMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Address, Address>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public Address MapAddress(BLL.App.DTO.Address inObject)
        {
            return Mapper.Map<Address>(inObject);
        }
    }
}