using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.Domain.Repositories;
using Contracts.Domain.Basic;

namespace BLL.Base.Services
{
    public class BaseEntityService<TServiceRepository, TUnitOfWork, TDALEntity, TBLLEntity> : BaseService,
        IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork
        where TServiceRepository : IBaseRepository<TDALEntity>
    {
        protected readonly TUnitOfWork ServiceUnitOfWork;
        protected readonly IBaseMapper<TDALEntity, TBLLEntity> Mapper;
        protected readonly TServiceRepository ServiceRepository;

        public BaseEntityService(TUnitOfWork unitOfWork, IBaseMapper<TDALEntity, TBLLEntity> mapper,
            TServiceRepository serviceRepository)
        {
            ServiceUnitOfWork = unitOfWork;
            ServiceRepository = serviceRepository;
            Mapper = mapper;

            // TODO - NOT POSSIBLE - we have no idea of what DAL actually is.
            // we have now BaseRepository implementation - cant call new on it
            // or asc for func methodToCreateRepo to create the correct repo
            //ServiceRepository = ServiceUnitOfWork.GetRepository<IBaseRepository<TDALEntity>>(methodToCreateRepo);
        }


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
    }
}
