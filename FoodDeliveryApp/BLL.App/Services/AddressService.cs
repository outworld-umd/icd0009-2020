using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AddressService : BaseEntityService<IAppUnitOfWork, IAddressRepository, IAddressServiceMapper, DAL.App.DTO.Address, Address>, IAddressService
    {
        public AddressService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Addresses, new AddressServiceMapper())
        {
            
        }
    }
}