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

    public class NutritionInfoRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.NutritionInfo, DTO.NutritionInfo>, INutritionInfoRepository {
        public NutritionInfoRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.NutritionInfo, DTO.NutritionInfo>()) { }
        
        // public override async Task<IEnumerable<NutritionInfo>> AllAsync() {
        //     return await RepoDbSet.Include(n => n.Item).ToListAsync();
        // }
        //
        // public override async Task<NutritionInfo> FindAsync(params object[] id) {
        //     return await RepoDbSet.Include(n => n.Item)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}