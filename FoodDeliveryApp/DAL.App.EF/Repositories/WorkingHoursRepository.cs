﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class WorkingHoursRepository : EFBaseRepository<WorkingHours, AppDbContext>, IWorkingHoursRepository {
        public WorkingHoursRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<WorkingHours>> AllAsync() {
            return await RepoDbSet.Include(w => w.Restaurant).ToListAsync();
        }

        public override async Task<WorkingHours> FindAsync(params object[] id) {
            return await RepoDbSet.Include(w => w.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}