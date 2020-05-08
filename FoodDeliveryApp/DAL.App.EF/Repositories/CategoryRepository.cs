﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories {

    public class CategoryRepository : EFBaseRepository<Category, AppDbContext>, ICategoryRepository {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext) { }
    }

}