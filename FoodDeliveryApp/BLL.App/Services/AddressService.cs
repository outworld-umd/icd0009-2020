using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AddressService : BaseEntityService<IAddressRepository, IAppUnitOfWork, DAL.App.DTO.Address, Address>, IAddressService
    {
        public AddressService(IAppUnitOfWork unitOfWork) : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Address, Address>(), unitOfWork.Addresses)
        {
        }
    }
}