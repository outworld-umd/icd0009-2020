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

    public class CategoryRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Category, Category>, ICategoryRepository {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Category, Category>()) { }
        
        public override async Task<IEnumerable<Category>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(с => с.RestaurantCategories)
                .ThenInclude(rc => rc.Restaurant)
                .ThenInclude(rc => rc!.RestaurantCategories)
                .ThenInclude(r => r.Category)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Category> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(с => с.RestaurantCategories)
                .ThenInclude(rc => rc.Restaurant)
                .ThenInclude(rc => rc!.RestaurantCategories)
                .ThenInclude(r => r.Category)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}