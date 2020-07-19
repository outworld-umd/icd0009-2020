using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF {

    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork, IBaseUnitOfWork where TDbContext : DbContext {
        protected TDbContext UOWDbContext;
    
        public EFBaseUnitOfWork(TDbContext uowDbContext) {
            UOWDbContext = uowDbContext;
        }

        public override int SaveChanges() {
            return UOWDbContext.SaveChanges();
        }

        public override Task<int> SaveChangesAsync() {
            return UOWDbContext.SaveChangesAsync();
        }
    }

}