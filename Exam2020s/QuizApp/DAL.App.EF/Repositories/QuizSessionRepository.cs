using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Domain.App.Enums;
using Domain.App.Identity;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuizSessionRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.QuizSession, QuizSession>,
        IQuizSessionRepository
    {
        public QuizSessionRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.QuizSession, QuizSession>())
        {
        }

        public override async Task<IEnumerable<QuizSession>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.Answers)
                .Include(a => a.AppUser)
                .Include(a => a.Quiz)
                .ThenInclude(a => a!.Questions)
                .ThenInclude(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<QuizSession> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.Answers)
                .Include(a => a.AppUser)
                .Include(a => a.Quiz)
                .ThenInclude(a => a!.Questions)
                .ThenInclude(a => a.Choices)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}