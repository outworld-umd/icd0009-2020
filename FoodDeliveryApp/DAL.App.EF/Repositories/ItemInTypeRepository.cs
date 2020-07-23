using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{


    public class ItemInTypeRepository : EFBaseRepository<AppDbContext, Domain.App.ItemInType, DTO.ItemInType>, IItemInTypeRepository {
        public ItemInTypeRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.ItemInType, DTO.ItemInType>()) { }
    }
}