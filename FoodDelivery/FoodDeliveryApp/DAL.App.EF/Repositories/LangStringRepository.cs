using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LangStringRepository :
        EFBaseRepository<AppDbContext, AppUser, Domain.App.LangString, DAL.App.DTO.LangString>,
        ILangStringRepository
    {
        public LangStringRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.LangString, DAL.App.DTO.LangString>())
        {
        }
    }
}