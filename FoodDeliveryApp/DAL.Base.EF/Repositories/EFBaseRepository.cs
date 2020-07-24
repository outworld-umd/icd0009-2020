using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.Domain.Repositories;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TUser, TDomainEntity, TDALEntity> : EFBaseRepository<Guid, TDbContext, TUser, TDomainEntity, TDALEntity>,
        IBaseRepository<TDALEntity>
        where TUser : IdentityUser<Guid>
        where TDbContext : DbContext, IBaseEntityTracker<Guid>
        where TDomainEntity : class, IDomainEntityIdMetadata<Guid>, new()
        where TDALEntity : class, IDomainEntityId<Guid>, new()

    {
        public EFBaseRepository(TDbContext dbContext,  IBaseDALMapper<TDomainEntity, TDALEntity> dalMapper) : base(dbContext, dalMapper)
        {
        }
    }

    public class EFBaseRepository<TKey, TDbContext, TUser,  TDomainEntity, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext, IBaseEntityTracker<TKey>
        where TUser : IdentityUser<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>, new()
        where TDALEntity : class, IDomainEntityId<TKey>, new()
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseDALMapper<TDomainEntity, TDALEntity> DALMapper;

        public EFBaseRepository(TDbContext dbContext, IBaseDALMapper<TDomainEntity, TDALEntity> dalMapper)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            DALMapper = dalMapper;
            if (RepoDbSet == null)
            {
                throw new ArgumentNullException(typeof(TDALEntity).Name + " was not found as DBSet!");
            }
        }
        
        public virtual async Task<IEnumerable<TDALEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }
        
        public virtual IEnumerable<TDALEntity> GetAll(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = query.ToList();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public virtual async Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = DALMapper.Map(domainEntity);
            return result;
        }

        public virtual TDALEntity Add(TDALEntity entity)
        {
            var domainEntity = DALMapper.Map(entity);
            var trackedDomainEntity = RepoDbSet.Add(domainEntity).Entity;
            RepoDbContext.AddToEntityTracker(trackedDomainEntity, entity);
            var result = DALMapper.Map(trackedDomainEntity);
            return result;
        }

        public virtual async Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = DALMapper.Map(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = DALMapper.Map(trackedDomainEntity);
            return result;
        }

        public virtual async Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = DALMapper.Map(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);
            return DALMapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task<TDALEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (domainEntity == null)
            {
                throw new ArgumentException("Entity to be updated was not found in data source!");
            }
            return DALMapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }
        
        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var recordExists = await query.AnyAsync(e => e.Id.Equals(id));
            return recordExists;
        }
        
        public bool Exists(TKey id, object? userId = null) {
            var query = PrepareQuery(userId, true);
            var recordExists = query.Any(e => e.Id.Equals(id));
            return recordExists;
        }

        public bool Any(Expression<Func<TDALEntity, bool>> predicate, object? userId = null) {
            var query = PrepareQuery(userId, true);
            var recordExists = query.Select(td => DALMapper.Map(td)).Any(predicate);
            return recordExists;
        }
        
        public async Task<bool> AnyAsync(Expression<Func<TDALEntity, bool>> predicate, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var recordExists = await query.Select(td => DALMapper.Map(td)).AnyAsync(predicate);
            return recordExists;
        }
        


        protected IQueryable<TDomainEntity> PrepareQuery(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            // Shall we disable entity tracking
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            // userId != null and is this entity implementing IDomainEntityUser
            if (userId != null && typeof(IDomainEntityUser<TKey, TUser>).IsAssignableFrom(typeof(TDomainEntity)))
            {
                // accessing TDomainEntity.AppUserId via shadow property access
                query = query.Where(e =>
                    Microsoft.EntityFrameworkCore.EF.Property<TKey>(e, nameof(IDomainEntityUser<TKey, TUser>.AppUserId))
                        .Equals((TKey) userId));
            }

            return query;
        }

        protected async Task CheckDomainEntityOwnership(TDomainEntity entity, object? userId = null)
        {
            var recordExists = await ExistsAsync(entity.Id, userId);
            if (!recordExists)
            {
                throw new ArgumentException("Entity to be updated was not found in data source!");
            }
        }
    }
}
