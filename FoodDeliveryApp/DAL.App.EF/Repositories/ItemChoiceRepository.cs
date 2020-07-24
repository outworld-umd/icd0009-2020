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

    public class ItemChoiceRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ItemChoice, ItemChoice>, IItemChoiceRepository {
        public ItemChoiceRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemChoice, ItemChoice>()) { }
        
        public override async Task<IEnumerable<ItemChoice>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(ic => ic.ItemOption)
                .ThenInclude(io => io.Item)
                .ThenInclude(i => i.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ThenInclude(it => it.Restaurant)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<ItemChoice> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(ic => ic.ItemOption)
                .ThenInclude(io => io.Item)
                .ThenInclude(i => i.ItemInTypes)
                .ThenInclude(iit => iit.ItemType)
                .ThenInclude(it => it.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);

        }
    }
}