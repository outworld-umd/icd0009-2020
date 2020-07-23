using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.App;

namespace DAL.App.EF.Repositories {

    public class CategoryRepository : EFBaseRepository<AppDbContext, Domain.App.Category, DAL.App.DTO.Category>, ICategoryRepository {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Category, DTO.Category>()) { }

    }

}