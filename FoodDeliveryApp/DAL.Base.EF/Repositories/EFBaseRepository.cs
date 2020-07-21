﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Mappers;
using Contracts.Domain;
using Contracts.Domain.Repositories;
using Contracts.Domain;
using Contracts.Domain.Basic;
using Contracts.Domain.Combined;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TDomainEntity, TDALEntity> : EFBaseRepository<Guid, TDbContext, TDomainEntity, TDALEntity>,
        IBaseRepository<TDALEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TDomainEntity : class, IDomainEntityIdMetadata<Guid>, new()
        where TDbContext : DbContext
    {
        public EFBaseRepository(TDbContext dbContext,  IBaseMapper<TDomainEntity, TDALEntity> mapper) : base(dbContext, mapper)
        {
        }
    }

    public class EFBaseRepository<TKey, TDbContext, TDomainEntity, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TDomainEntity : class, IDomainEntityIdMetadata<TKey>, new()
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected TDbContext RepoDbContext;
        protected DbSet<TDomainEntity> RepoDbSet;
        protected IBaseMapper<TDomainEntity, TDALEntity> Mapper;

        public EFBaseRepository(TDbContext dbContext, IBaseMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            Mapper = mapper;
            if (RepoDbSet == null)
            {
                throw new ArgumentNullException(typeof(TDALEntity).Name + " was not found as DBSet!");
            }
        }

        public virtual IEnumerable<TDALEntity> All()
        {
            return RepoDbSet.ToList().Select(domainEntity => Mapper.Map(domainEntity));
        }

        public virtual async Task<IEnumerable<TDALEntity>> AllAsync()
        {
            return (await RepoDbSet.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public IEnumerable<TDALEntity> Get(Expression<Func<TDALEntity, bool>>? filter = null) {
            return RepoDbSet.Select(domainEntity => Mapper.Map(domainEntity)).Where(filter).ToList();
        }

        public async Task<IEnumerable<TDALEntity>> GetAsync(Expression<Func<TDALEntity, bool>>? filter = null) {
            return await RepoDbSet.Select(domainEntity => Mapper.Map(domainEntity)).Where(filter).ToListAsync();
        }

        public virtual TDALEntity Find(params object[] id)
        {
            return Mapper.Map(RepoDbSet.Find(id));
        }

        public virtual async Task<TDALEntity> FindAsync(params object[] id)
        {
            return Mapper.Map(await RepoDbSet.FindAsync(id));
        }

        public virtual TDALEntity Add(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)).Entity);
        }

        public virtual TDALEntity Update(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)).Entity);
        }

        public virtual TDALEntity Remove(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)).Entity);
        }

        public virtual TDALEntity Remove(params object[] id)
        {
            return Mapper.Map(RepoDbSet.Remove(RepoDbSet.Find(id)).Entity);
        }

        public bool Any(Expression<Func<TDALEntity, bool>> predicate) {
            return RepoDbSet.Select(domainEntity => Mapper.Map(domainEntity)).Any(predicate);
        }
    }
}
