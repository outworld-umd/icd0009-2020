using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class WorkingHoursRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.WorkingHours, WorkingHours>, IWorkingHoursRepository {
        public WorkingHoursRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.WorkingHours, WorkingHours>()) { }
        
        public override async Task<IEnumerable<WorkingHours>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(w => w.Restaurant)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<WorkingHours> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(w => w.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }

}