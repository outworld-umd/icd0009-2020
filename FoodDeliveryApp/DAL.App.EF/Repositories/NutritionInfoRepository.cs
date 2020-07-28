using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class NutritionInfoRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.NutritionInfo, NutritionInfo>, INutritionInfoRepository {
        public NutritionInfoRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.NutritionInfo, NutritionInfo>()) { }
        
        public override async Task<IEnumerable<NutritionInfo>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(n => n.Item)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<NutritionInfo> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(n => n.Item)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}