using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRepository : EFBaseRepository<Order, AppDbContext>, IOrderRepository {
        public OrderRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<Order>> AllAsync() {
            return await RepoDbSet
                .Include(o => o.Restaurant).ToListAsync();
        }

        public override async Task<Order> FindAsync(params object[] id) {
            return await RepoDbSet
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }
    }

}