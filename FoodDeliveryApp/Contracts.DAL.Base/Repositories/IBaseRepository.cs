using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TDALEntity> : IBaseRepository<Guid, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<Guid>, new() 
    {
    }


    public interface IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        // crud
        IEnumerable<TDALEntity> All();
        Task<IEnumerable<TDALEntity>> AllAsync();

        // TODO: would be nice to implement these predicates
        IEnumerable<TDALEntity> Get(Expression<Func<TDALEntity, bool>>? filter = null);
        Task<IEnumerable<TDALEntity>> GetAsync(Expression<Func<TDALEntity, bool>>? filter = null);

        TDALEntity Find(params object[] id);
        Task<TDALEntity> FindAsync(params object[] id);
        TDALEntity Add(TDALEntity entity);
        TDALEntity Update(TDALEntity entity);
        TDALEntity Remove(TDALEntity entity);
        TDALEntity Remove(params object[] id);

        public bool Any(Expression<Func<TDALEntity, bool>> predicate);
    }
}