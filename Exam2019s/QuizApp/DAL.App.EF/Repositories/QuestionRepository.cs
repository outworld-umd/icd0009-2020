using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Domain.App.Identity;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuestionRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.Question, Question>,
        IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Question, Question>())
        {
        }

        public override async Task<IEnumerable<Question>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.CorrectChoice)
                .Include(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .Include(a => a.Quiz)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Question> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.CorrectChoice)
                .Include(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .Include(a => a.Quiz)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}