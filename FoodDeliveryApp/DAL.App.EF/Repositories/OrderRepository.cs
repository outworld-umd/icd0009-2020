using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DTO.Order>, IOrderRepository {
        public OrderRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Order, DTO.Order>()) { }
        
        public override async Task<IEnumerable<Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.OrderItemChoices).ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Order> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.OrderItemChoices).FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }

}