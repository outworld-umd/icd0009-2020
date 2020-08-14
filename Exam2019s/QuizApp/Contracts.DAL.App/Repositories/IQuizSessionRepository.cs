using System;
using ee.itcollege.anguzo.Contracts.DAL.Base.Repositories;
using ee.itcollege.anguzo.Contracts.Domain.Base.Basic;
using DAL.App.DTO;
using Domain.App.Enums;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizSessionRepository : IQuizSessionRepository<Guid, QuizSession>, IBaseRepository<QuizSession>
    {
    }

    public interface IQuizSessionRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TKey : IEquatable<TKey>
    {
    }
}