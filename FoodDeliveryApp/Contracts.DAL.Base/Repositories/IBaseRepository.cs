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
        IEnumerable<TDALEntity> GetAll(object? userId = null, bool noTracking = true);

        Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true);
        TDALEntity Add(TDALEntity entity);
        Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null);
        Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null);
        Task<TDALEntity> RemoveAsync(TKey id, object? userId = null);
        Task<bool> ExistsAsync(TKey id, object? userId = null);
        bool Exists(TKey id, object? userId = null);
        Task<bool> AnyAsync(Expression<Func<TDALEntity, bool>> predicate, object? userId = null);
        bool Any(Expression<Func<TDALEntity, bool>> predicate, object? userId = null);
    }
}