﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class RestaurantUserRepository : EFBaseRepository<RestaurantUser, AppDbContext>, IRestaurantUserRepository {

        public RestaurantUserRepository(AppDbContext dbContext) : base(dbContext) { }
        public override async Task<IEnumerable<RestaurantUser>> AllAsync() {
            return await RepoDbSet.Include(r => r.AppUser)
                .Include(r => r.Restaurant).ToListAsync();
        }

        public override async Task<RestaurantUser> FindAsync(params object[] id) {
            return await RepoDbSet.Include(r => r.AppUser)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}