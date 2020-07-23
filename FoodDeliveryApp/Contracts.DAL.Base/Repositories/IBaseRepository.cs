using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Domain;
using Contracts.Domain.Basic;

namespace Contracts.Domain.Repositories
{
    public interface IBaseRepository<TDALEntity> : IBaseRepository<Guid, TDALEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new() 
    {
    }


    public interface IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> GetAllAsync(object? userId = null, bool noTracking = true);
        Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true);
        TDALEntity Add(TDALEntity entity);
        Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null);
        Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null);
        Task<TDALEntity> RemoveAsync(TKey id, object? userId = null);
        Task<bool> ExistsAsync(TKey id, object? userId = null);
        
        // // crud
        // IEnumerable<TDALEntity> All();
        // Task<IEnumerable<TDALEntity>> AllAsync();
        //
        // // TODO: would be nice to implement these predicates
        // IEnumerable<TDALEntity> Get(Expression<Func<TDALEntity, bool>>? filter = null);
        // Task<IEnumerable<TDALEntity>> GetAsync(Expression<Func<TDALEntity, bool>>? filter = null);
        //
        // TDALEntity Find(params object[] id);
        // Task<TDALEntity> FindAsync(params object[] id);
        // TDALEntity Add(TDALEntity entity);
        // TDALEntity Update(TDALEntity entity);
        // TDALEntity Remove(TDALEntity entity);
        // TDALEntity Remove(params object[] id);
        //
        // public bool Any(Expression<Func<TDALEntity, bool>> predicate);
    }
}