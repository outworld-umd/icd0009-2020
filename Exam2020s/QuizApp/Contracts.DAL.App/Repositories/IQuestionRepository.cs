using System;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionRepository : IQuestionRepository<Guid, Question>, IBaseRepository<Question>
    {
    }

    public interface IQuestionRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TKey : IEquatable<TKey>
    {
    }
}