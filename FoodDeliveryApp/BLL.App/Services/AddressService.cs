using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<BLL.App.DTO.Address>> AllAsync(Guid? userId = null)=>
            (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Address> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) => await ServiceRepository.ExistsAsync(id, userId);

        public async Task DeleteAsync(Guid id, Guid? userId = null) => await ServiceRepository.DeleteAsync(id, userId);
    }
}