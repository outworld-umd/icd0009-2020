using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using ee.itcollege.anguzo.Domain.Identity;using ee.itcollege.anguzo.DAL.Base.EF.Repositories;

using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class ItemOptionRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.ItemOption, ItemOption>, IItemOptionRepository {
        public ItemOptionRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemOption, ItemOption>()) { }
        
        public override async Task<IEnumerable<ItemOption>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(io => io.Item)
                .ThenInclude(i => i!.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ThenInclude(it => it!.Restaurant)
                .Include(io => io.ItemChoices)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<ItemOption> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(io => io.Item)
                .ThenInclude(i => i!.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ThenInclude(it => it!.Restaurant)
                .Include(io => io.ItemChoices)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}