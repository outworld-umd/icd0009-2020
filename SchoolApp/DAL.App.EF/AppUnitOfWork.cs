using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork {

        public IGradeRepository Grades => GetRepository(() => new GradeRepository(UOWDbContext));
        public IBaseRepository<Person> Persons => GetRepository(() => new EFBaseRepository<Person, DbContext>(UOWDbContext));

        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext) {}
        
    }

}