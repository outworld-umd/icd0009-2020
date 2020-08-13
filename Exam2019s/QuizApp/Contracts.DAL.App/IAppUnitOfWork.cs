using Contracts.DAL.App.Repositories;
using ee.itcollege.anguzo.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        public IAnswerRepository Answers { get; }
        public IChoiceRepository Choices { get; }
        public IQuestionRepository Questions { get; }
        public IQuizRepository Quizzes { get; }
    }
}