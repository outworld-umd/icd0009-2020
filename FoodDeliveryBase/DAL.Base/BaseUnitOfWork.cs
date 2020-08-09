using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.anguzo.Contracts.DAL.Base;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

namespace ee.itcollege.anguzo.DAL.Base
{
    public abstract class BaseUnitOfWork<TKey> : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();
        
        private readonly Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> _entityTracker =
            new Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>>();

        public abstract int SaveChanges();

        public abstract Task<int> SaveChangesAsync();

        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            var newRepoInstance = repoCreationMethod();
            _repoCache.Add(typeof(TRepository), newRepoInstance!);
            return newRepoInstance;
        }

        public void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        protected void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}