using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class ItemTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ItemType, DTO.ItemType>, IItemTypeRepository {
        public ItemTypeRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemType, DTO.ItemType>()) { }
        
        // public override async Task<IEnumerable<ItemType>> AllAsync() {
        //     return await RepoDbSet.Include(i => i.Restaurant).ToListAsync();
        // }
        //
        // public override async Task<ItemType> FindAsync(params object[] id) {
        //     return await RepoDbSet.Include(i => i.Restaurant)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}