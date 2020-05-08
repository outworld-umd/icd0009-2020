using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class ItemTypeRepository : EFBaseRepository<ItemType, AppDbContext>, IItemTypeRepository {
        public ItemTypeRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<ItemType>> AllAsync() {
            return await RepoDbSet.Include(i => i.Restaurant).ToListAsync();
        }

        public override async Task<ItemType> FindAsync(params object[] id) {
            return await RepoDbSet.Include(i => i.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}