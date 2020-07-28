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

    public class ItemTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ItemType, ItemType>, IItemTypeRepository {
        public ItemTypeRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemType, ItemType>()) { }
       
        public override async Task<IEnumerable<ItemType>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(it => it.ItemInTypes)
                .ThenInclude(iit => iit.Item)
                .Include(it => it.Restaurant)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<ItemType> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(it => it.ItemInTypes)
                .ThenInclude(iit => iit.Item)
                .Include(it => it.Restaurant)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}