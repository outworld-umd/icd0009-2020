using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class LangStrRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.LangStr, DAL.App.DTO.LangStr>,
        ILangStrRepository
    {
        public LangStrRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.LangStr, DAL.App.DTO.LangStr>())
        {
        }
    }
}