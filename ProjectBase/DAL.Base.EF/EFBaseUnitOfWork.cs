﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ee.itcollege.anguzo.DAL.Base.EF {

    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected readonly TDbContext UOWDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            this.UOWDbContext = uowDbContext;
        }

        public override int SaveChanges() {
            return UOWDbContext.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            int num = await UOWDbContext.SaveChangesAsync(new CancellationToken());
            UpdateTrackedEntities();
            return num;
        }
    }

}