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
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override IEnumerable<Category> GetAll(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = query
                .Include(с => с.RestaurantCategories)
                .ThenInclude(rc => rc.Restaurant)
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations)
                .ToList();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Category> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(с => с.RestaurantCategories)
                .ThenInclude(rc => rc.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
        
        public override async Task<Category> UpdateAsync(Category entity, object? userId = null)
        {
            var domainEntity = DALMapper.Map(entity);

            // fix the language string - from mapper we get new ones - so duplicate values will be created in db
            // load back from db the originals 
            domainEntity.Name = await RepoDbContext.LangStrings.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.NameId);
            domainEntity.Name.SetTranslation(entity.Name);

            await CheckDomainEntityOwnership(domainEntity, userId);
            
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = DALMapper.Map(trackedDomainEntity);
            return result;        
        }
    }
}