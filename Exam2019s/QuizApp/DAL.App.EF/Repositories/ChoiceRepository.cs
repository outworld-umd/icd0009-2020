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
    public class ChoiceRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.Choice, Choice>,
        IChoiceRepository
    {
        public ChoiceRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Choice, Choice>())
        {
        }

        public override async Task<IEnumerable<Choice>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.Question)
                .Include(a => a.Answers)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Choice> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.Question)
                .Include(a => a.Answers)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}