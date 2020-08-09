using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using ee.itcollege.anguzo.Contracts.DAL.Base;

using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;

using Microsoft.EntityFrameworkCore.Internal;

namespace BLL.Base.Services
{
    public abstract class BaseEntityService<TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity> :
        BaseEntityService<Guid, TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
        where TRepository : IBaseRepository<Guid, TDALEntity>
        where TMapper : IBaseBLLMapper<TDALEntity, TBLLEntity>
    {
        public BaseEntityService(TUnitOfWork uow, TRepository repository,
            TMapper mapper) : base(uow, repository, mapper)
        {
        }
    }
    
    public abstract class BaseEntityService<TKey, TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity> :
        IBaseEntityService<TKey, TBLLEntity>
        where TKey : IEquatable<TKey>
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TRepository : IBaseRepository<TKey, TDALEntity>
        where TMapper : IBaseBLLMapper<TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TBLLEntity : class, IDomainEntityId<TKey>, new()
    {
        protected readonly TUnitOfWork ServiceUnitOfWork;
        protected readonly IBaseBLLMapper<TDALEntity, TBLLEntity> BLLMapper;
        protected readonly TRepository ServiceRepository;
        protected readonly ExpressionMapper<TBLLEntity, TDALEntity> ExpressionMapper;

        public BaseEntityService(TUnitOfWork unitOfWork, TRepository serviceRepository, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper)
        {
            ServiceUnitOfWork = unitOfWork;
            ServiceRepository = serviceRepository;
            BLLMapper = mapper;
            ExpressionMapper = new ExpressionMapper<TBLLEntity, TDALEntity>();
        }
        
        public virtual async Task<IEnumerable<TBLLEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await ServiceRepository.GetAllAsync(userId, noTracking);
            var result = dalEntities.Select(e => BLLMapper.Map(e));
            return result;
        }

        public IEnumerable<TBLLEntity> GetAll(object? userId = null, bool noTracking = true)
        {
            var dalEntities = ServiceRepository.GetAll(userId, noTracking);
            var result = dalEntities.Select(e => BLLMapper.Map(e));
            return result;  
        }

        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true)
        {
            var dalEntity = await ServiceRepository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = BLLMapper.Map(dalEntity);
            return result;        }

        public TBLLEntity Add(TBLLEntity entity)
        {
            var dalEntity = BLLMapper.Map(entity);
            var trackedDALEntity = ServiceRepository.Add(dalEntity);
            ServiceUnitOfWork.AddToEntityTracker(trackedDALEntity, entity);
            var result = BLLMapper.Map(trackedDALEntity);
            
            return result;
        }

        public virtual async Task<TBLLEntity> UpdateAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = BLLMapper.Map(entity);
            var resultDALEntity = await ServiceRepository.UpdateAsync(dalEntity, userId);
            var result = BLLMapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = BLLMapper.Map(entity);
            var resultDALEntity = await ServiceRepository.RemoveAsync(dalEntity, userId);
            var result = BLLMapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var resultDALEntity = await ServiceRepository.RemoveAsync(id, userId);
            var result = BLLMapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var result = await ServiceRepository.ExistsAsync(id, userId);
            return result;
        }

        public bool Exists(TKey id, object? userId = null)
        {
            var result = ServiceRepository.Exists(id, userId);
            return result; 
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null) {
            var dalEntityPredicate = ExpressionMapper.Convert(predicate);
            var result = await ServiceRepository.AnyAsync(dalEntityPredicate, userId);
            return result;
        }

        public virtual bool Any(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null)
        {
            var dalEntityPredicate = ExpressionMapper.Convert(predicate);
            var result = ServiceRepository.Any(dalEntityPredicate, userId);
            return result;
        }


        /*
        public virtual IEnumerable<TBLLEntity> All() =>
            ServiceRepository.All().Select(entity => Mapper.Map(entity));
        
        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select(entity => Mapper.Map(entity));
        
        public virtual IEnumerable<TBLLEntity> Get(Expression<Func<TBLLEntity, bool>>? filter = null) =>
            All().Where(filter?.Compile());
        
        public virtual async Task<IEnumerable<TBLLEntity>> GetAsync(Expression<Func<TBLLEntity, bool>>? filter = null) =>
            (await AllAsync()).Where(filter?.Compile());
        
        public virtual TBLLEntity Find(params object[] id) =>
            Mapper.Map(ServiceRepository.Find(id));
        
        public virtual async Task<TBLLEntity> FindAsync(params object[] id) =>
            Mapper.Map(await ServiceRepository.FindAsync(id));
        
        public virtual TBLLEntity Add(TBLLEntity entity) =>
            Mapper.Map(ServiceRepository.Add(Mapper.Map(entity)));
        
        public virtual TBLLEntity Update(TBLLEntity entity) =>
            Mapper.Map(ServiceRepository.Update(Mapper.Map(entity)));
        
        
        public virtual TBLLEntity Remove(TBLLEntity entity) =>
            Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)));
        
        
        public virtual TBLLEntity Remove(params object[] id) =>
            Mapper.Map(ServiceRepository.Remove(id));
        
        public bool Any(Expression<Func<TBLLEntity, bool>> predicate) =>
            All().Any(predicate.Compile());
        */
    }
}
