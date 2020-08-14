using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuizRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.Quiz, Quiz>, IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Quiz, Quiz>())
        {
        }

        public override async Task<IEnumerable<Quiz>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.AppUser)
                .Include(a => a.Questions)
                .ThenInclude(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .Include(e => e.QuizSessions)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override IEnumerable<Quiz> GetAll(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = query
                .Include(a => a.AppUser)
                .Include(a => a.Questions)
                .ThenInclude(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .Include(e => e.QuizSessions)
                .ToList();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Quiz> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.AppUser)
                .Include(a => a.Questions)
                .ThenInclude(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .Include(e => e.QuizSessions)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}