using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using ee.itcollege.anguzo.Contracts.Domain.Base;

using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
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