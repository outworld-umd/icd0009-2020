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

namespace DAL.App.EF.Repositories {

    public class RestaurantRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.Restaurant, DTO.Restaurant>, IRestaurantRepository {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Restaurant, DTO.Restaurant>())
        {
            
        }

        public override async Task<IEnumerable<Restaurant>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(r => r.RestaurantCategories)
                .ThenInclude(rc => rc.Category)
                .ThenInclude(l => l!.Name)
                .ThenInclude(t => t!.Translations)
                .Include(r => r.WorkingHourses)
                .Include(r => r.ItemTypes)
                .ThenInclude(it => it.ItemInTypes)
                .ThenInclude(iit => iit.Item)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Restaurant> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(r => r.RestaurantCategories)
                .ThenInclude(rc => rc.Category)
                .ThenInclude(l => l!.Name)
                .ThenInclude(t => t!.Translations)
                .Include(r => r.WorkingHourses)
                .Include(r => r.ItemTypes)
                .ThenInclude(it => it.ItemInTypes)
                .ThenInclude(iit => iit.Item)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}