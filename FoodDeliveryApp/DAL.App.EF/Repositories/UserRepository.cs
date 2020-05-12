using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.Identity;

namespace DAL.App.EF.Repositories {

    public class UserRepository : EFBaseRepository<AppUser, AppDbContext>, IUserRepository {

        public UserRepository(AppDbContext dbContext) : base(dbContext) { }
    }

}