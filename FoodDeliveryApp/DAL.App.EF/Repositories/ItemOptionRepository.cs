using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class ItemOptionRepository : EFBaseRepository<ItemOption, AppDbContext>, IItemOptionRepository {
        public ItemOptionRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<ItemOption>> AllAsync() {
            return await RepoDbSet.Include(i => i.Item).ToListAsync();
        }

        public override async Task<ItemOption> FindAsync(params object[] id) {
            return await RepoDbSet.Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}