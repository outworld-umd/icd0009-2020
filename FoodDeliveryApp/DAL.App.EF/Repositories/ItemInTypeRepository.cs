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


    public class ItemInTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ItemInType, DTO.ItemInType>, IItemInTypeRepository {
        public ItemInTypeRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemInType, DTO.ItemInType>()) { }
        
        public override async Task<IEnumerable<ItemInType>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(io => io!.Item)
                .Include(iit => iit.ItemType)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<ItemInType> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(io => io!.Item)
                .Include(iit => iit.ItemType)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);

        }
    }
}