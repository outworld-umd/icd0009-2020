using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRowRepository : EFBaseRepository<AppDbContext, Domain.OrderRow, DTO.OrderRow>, IOrderRowRepository {
        public OrderRowRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.OrderRow, DTO.OrderRow>()) { }
        
        // public override async Task<IEnumerable<OrderRow>> AllAsync() {
        //     return await RepoDbSet
        //         .Include(o => o.Item)
        //         .Include(o => o.Order).ToListAsync();
        // }
        //
        // public override async Task<OrderRow> FindAsync(params object[] id) {
        //     return await RepoDbSet
        //         .Include(o => o.Item)
        //         .Include(o => o.Order)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}