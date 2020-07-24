using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DTO.Order>, IOrderRepository {
        public OrderRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Order, DTO.Order>()) { }
        
        // public override async Task<IEnumerable<Order>> AllAsync() {
        //     return await RepoDbSet
        //         .Include(o => o.Restaurant).ToListAsync();
        // }
        //
        // public override async Task<Order> FindAsync(params object[] id) {
        //     return await RepoDbSet
        //         .Include(o => o.Restaurant)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}