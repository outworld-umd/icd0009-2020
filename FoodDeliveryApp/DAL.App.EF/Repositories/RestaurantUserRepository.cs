using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class RestaurantUserRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.RestaurantUser, RestaurantUser>, IRestaurantUserRepository {

        public RestaurantUserRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.RestaurantUser, RestaurantUser>()) { }
        
        public override async Task<IEnumerable<RestaurantUser>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(r => r.AppUser)
                .Include(r => r.Restaurant)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<RestaurantUser> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(r => r.AppUser)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}