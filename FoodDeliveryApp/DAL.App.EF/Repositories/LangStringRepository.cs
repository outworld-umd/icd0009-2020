using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class LangStringRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.LangString, DAL.App.DTO.LangString>,
        ILangStringRepository
    {
        public LangStringRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.LangString, DAL.App.DTO.LangString>())
        {
        }
    }
}