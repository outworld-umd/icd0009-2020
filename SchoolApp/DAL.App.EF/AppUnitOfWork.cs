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
        public IBaseRepository<DependenceType> DependenceTypes => GetRepository(() => new EFBaseRepository<DependenceType, DbContext>(UOWDbContext));
        public IBaseRepository<Form> Forms => GetRepository(() => new EFBaseRepository<Form, DbContext>(UOWDbContext));
        public IBaseRepository<FormRole> FormRoles => GetRepository(() => new EFBaseRepository<FormRole, DbContext>(UOWDbContext));
        public IGradeRepository Grades => GetRepository(() => new GradeRepository(UOWDbContext));
        public IBaseRepository<GradeType> GradeTypes => GetRepository(() => new EFBaseRepository<GradeType, DbContext>(UOWDbContext));
        public IBaseRepository<Person> Persons => GetRepository(() => new EFBaseRepository<Person, DbContext>(UOWDbContext));
        public IBaseRepository<RemarkType> RemarkTypes => GetRepository(() => new EFBaseRepository<RemarkType, DbContext>(UOWDbContext));
        public IBaseRepository<Subject> Subjects => GetRepository(() => new EFBaseRepository<Subject, DbContext>(UOWDbContext));

        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext) {
        }
    }

}
