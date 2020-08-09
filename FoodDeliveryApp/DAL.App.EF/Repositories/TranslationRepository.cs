using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TranslationRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Translation,
            DAL.App.DTO.Translation>,
        ITranslationRepository
    {
        public TranslationRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.Translation, DTO.Translation>())
        {
            
        }
        public override async Task<IEnumerable<Translation>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(t => t.LangString)
                .ThenInclude(l => l!.CategoryNames)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Translation> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(t => t.LangString)
                .ThenInclude(l => l!.CategoryNames)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}