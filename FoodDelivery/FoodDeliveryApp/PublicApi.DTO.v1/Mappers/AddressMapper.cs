using AutoMapper;
using PublicApi.DTO.v1.Mappers.Base;
using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class AddressMapper : APIMapper<BLL.App.DTO.Address, Address>
    {
        public Address MapAddress(BLL.App.DTO.Address inObject)
        {
            return Mapper.Map<Address>(inObject);
        }
    }
}