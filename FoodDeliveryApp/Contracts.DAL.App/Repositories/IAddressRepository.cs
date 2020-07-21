using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain;
using Contracts.Domain.Basic;
using Contracts.Domain.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories {


    public interface IAddressRepository : IAddressRepository<Guid, Address>, IBaseRepository<Address>
    {
        
    }

    public interface IAddressRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainEntityId<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? userId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }

}