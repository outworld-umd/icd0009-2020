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

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Item, Item>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Item, Item>()) { }

        public override async Task<IEnumerable<Item>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(i => i.NutritionInfos)
                .Include(i => i.ItemOptions)
                .ThenInclude(io => io.ItemChoices)
                .Include(i => i.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Item> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(i => i.ItemOptions)
                .Include(i => i.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }

        public async Task<IEnumerable<Item>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true) {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query.Where(item => item.RestaurantId.Equals(restaurantId))
                .Include(i => i.NutritionInfos)
                .Include(i => i.ItemOptions)
                .ThenInclude(io => io.ItemChoices)
                .Include(i => i.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }
    }
}