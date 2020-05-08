using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class RestaurantCategoryRepository : EFBaseRepository<RestaurantCategory, AppDbContext>, IRestaurantCategoryRepository {
        public RestaurantCategoryRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<RestaurantCategory>> AllAsync() {
            return await RepoDbSet
                .Include(r => r.Category)
                .Include(r => r.Restaurant).ToListAsync();
        }

        public override async Task<RestaurantCategory> FindAsync(params object[] id) {
            return await RepoDbSet
                .Include(r => r.Category)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}