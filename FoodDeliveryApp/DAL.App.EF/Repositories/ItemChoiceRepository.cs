﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class ItemChoiceRepository : EFBaseRepository<ItemChoice, AppDbContext>, IItemChoiceRepository {
        public ItemChoiceRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<ItemChoice>> AllAsync() {
            return await RepoDbSet.Include(i => i.ItemOption).ToListAsync();
        }

        public override async Task<ItemChoice> FindAsync(params object[] id) {
            return await RepoDbSet.Include(i => i.ItemOption)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}