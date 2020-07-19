using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderItemChoiceRepository : EFBaseRepository<AppDbContext, Domain.App.OrderItemChoice, DTO.OrderItemChoice>, IOrderItemChoiceRepository {
        public OrderItemChoiceRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Domain.App.OrderItemChoice, DTO.OrderItemChoice>()) { }
        
        // public override async Task<IEnumerable<OrderItemChoice>> AllAsync() {
        //     return await RepoDbSet
        //         .Include(o => o.ItemChoice)
        //         .Include(o => o.OrderRow).ToListAsync();
        // }
        //
        // public override async Task<OrderItemChoice> FindAsync(params object[] id) {
        //     return await RepoDbSet
        //         .Include(o => o.ItemChoice)
        //         .Include(o => o.OrderRow)
        //         .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        // }
    }

}