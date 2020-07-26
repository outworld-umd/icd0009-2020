using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class AddressServiceMapper: BaseBLLMapper<DALAppDTO.Address, BLLAppDTO.Address>, IAddressServiceMapper
    {
        public AddressServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Address, DALAppDTO.Address>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Address, BLLAppDTO.Address>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}