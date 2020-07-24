using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.Domain.Repositories;
using Contracts.Domain.Basic;
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

        public BaseEntityService(TUnitOfWork unitOfWork, TRepository serviceRepository, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper)
        {
            ServiceUnitOfWork = unitOfWork;
            ServiceRepository = serviceRepository;
            BLLMapper = mapper;
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

        public Task<bool> AnyAsync(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null)
        {
            throw new NotImplementedException();
        }

        // TODO AnyAsync
        // public async Task<bool> AnyAsync(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null)
        // {
        //     var result = await ServiceRepository.AnyAsync(predicate, userId);
        //     return result; 
        // }
        //
        // public bool Any(Expression<Func<TBLLEntity, bool>> predicate, object? userId = null)
        // {
        //     var result = ServiceRepository.Any(predicate, userId);
        //     return result; 
        // }
        //
        // internal class ExpressionConverter<TInput, TOutput> : ExpressionVisitor
        // {
        //     public Expression<Func<TOutput, bool>> Convert(Expression<Func<TInput, bool>> expression)
        //     {
        //         return (Expression<Func<TOutput, bool>>)Visit(expression);
        //     }
        //
        //     private ParameterExpression replaceParam;
        //
        //     protected override Expression VisitLambda<T>(Expression<T> node)
        //     {
        //         if (typeof(T) == typeof(Func<TInput, bool>))
        //         {
        //             replaceParam = Expression.Parameter(typeof(TOutput), "p");
        //             return Expression.Lambda<Func<TOutput, bool>>(Visit(node.Body), replaceParam);
        //         }
        //         return base.VisitLambda<T>(node);
        //     }
        //
        //     protected override Expression VisitParameter(ParameterExpression node)
        //     {
        //         if (node.Type == typeof(TInput))
        //             return replaceParam;
        //         return base.VisitParameter(node);
        //     }
        //
        //     protected override Expression VisitMember(MemberExpression node)
        //     {
        //         if (node.Member.DeclaringType == typeof(TInput))
        //         {
        //             var member = typeof(TOutput).GetMember(node.Member.Name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).FirstOrDefault();
        //             if (member == null)
        //                 throw new InvalidOperationException("Cannot identify corresponding member of DataObject");
        //             return Expression.MakeMemberAccess(Visit(node.Expression), member);
        //         }
        //         return base.VisitMember(node);
        //     }
        // }


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
