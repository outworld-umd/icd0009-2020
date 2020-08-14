using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.anguzo.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public IQuizRepository Quizzes => GetRepository<IQuizRepository>(() => new QuizRepository(UOWDbContext));

        public IQuestionRepository Questions =>
            GetRepository<IQuestionRepository>(() => new QuestionRepository(UOWDbContext));

        public IChoiceRepository Choices => GetRepository<IChoiceRepository>(() => new ChoiceRepository(UOWDbContext));
        public IAnswerRepository Answers => GetRepository<IAnswerRepository>(() => new AnswerRepository(UOWDbContext));

        public IQuizSessionRepository QuizSessions =>
            GetRepository<IQuizSessionRepository>(() => new QuizSessionRepository(UOWDbContext));

        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
    }
}