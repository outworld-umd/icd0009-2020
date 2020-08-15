using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {
    
    public class RestaurantCategoryRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.RestaurantCategory, RestaurantCategory>, IRestaurantCategoryRepository {
        public RestaurantCategoryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.RestaurantCategory, RestaurantCategory>()) { }
        
        public override async Task<IEnumerable<RestaurantCategory>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(r => r.Category)
                .ThenInclude(l => l!.Name)
                .ThenInclude(t => t!.Translations)
                .Include(r => r.Restaurant)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<RestaurantCategory> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(r => r.Category)
                .ThenInclude(l => l!.Name)
                .ThenInclude(t => t!.Translations)                
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}