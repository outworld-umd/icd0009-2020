using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DTO.Order>, IOrderRepository {
        public OrderRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.Order, DTO.Order>()) { }
        
        public override async Task<IEnumerable<Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.AppUser)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.OrderItemChoices)
                .ThenInclude(oic => oic.ItemChoice)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Order> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.AppUser)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.OrderItemChoices)
                .ThenInclude(oic => oic.ItemChoice)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }

        public async Task<IEnumerable<Order>> GetAllByRestaurantAsync(Guid restaurantId, object? userId = null, bool noTracking = true) {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query.Where(order => order.RestaurantId.Equals(restaurantId))
                .Include(a => a.AppUser)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.OrderItemChoices)
                .ThenInclude(oic => oic.ItemChoice)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }
    }
}