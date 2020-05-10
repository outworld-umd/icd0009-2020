using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{


    public class ItemInTypeRepository : EFBaseRepository<ItemInType, AppDbContext>, IItemInTypeRepository {
        public ItemInTypeRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}