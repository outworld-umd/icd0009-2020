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
    }

}