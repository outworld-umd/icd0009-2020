﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF {

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
            EFBaseUnitOfWork<TKey, TDbContext> efBaseUnitOfWork = this;
            int num = await efBaseUnitOfWork.UOWDbContext.SaveChangesAsync(new CancellationToken());
            efBaseUnitOfWork.UpdateTrackedEntities();
            return num;
        }
    }

}