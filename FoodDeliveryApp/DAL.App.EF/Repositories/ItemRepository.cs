﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : EFBaseRepository<AppDbContext, Item, DTO.Item>, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Item, DTO.Item>()) { }


        // public override async Task<IEnumerable<Item>> AllAsync() {
        //     return await RepoDbSet.Include(i => i.ItemType).ToListAsync();
        // }
        //
        // public override async Task<Item> FindAsync(params object[] id) {
        //     return await RepoDbSet.Include(i => i.ItemType)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }
}