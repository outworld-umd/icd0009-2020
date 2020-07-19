using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories {

    public class RestaurantUserRepository : EFBaseRepository<AppDbContext, Domain.App.RestaurantUser, DTO.RestaurantUser>, IRestaurantUserRepository {

        public RestaurantUserRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Domain.App.RestaurantUser, DTO.RestaurantUser>()) { }
        // public override async Task<IEnumerable<RestaurantUser>> AllAsync() {
        //     return await RepoDbSet.Include(r => r.AppUser)
        //         .Include(r => r.Restaurant).ToListAsync();
        // }
        //
        // public override async Task<RestaurantUser> FindAsync(params object[] id) {
        //     return await RepoDbSet.Include(r => r.AppUser)
        //         .Include(r => r.Restaurant)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}