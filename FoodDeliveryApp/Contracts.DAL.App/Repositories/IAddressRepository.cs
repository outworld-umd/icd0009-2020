using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {

    public interface IAddressRepository : IBaseRepository<Address> {
        Task<IEnumerable<Address>> AllAsync(Guid? userId = null);
    }

}