using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderRowRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.OrderRow, OrderRow>, IOrderRowRepository {
        public OrderRowRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.OrderRow, OrderRow>()) { }
        
        public override async Task<IEnumerable<OrderRow>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(or => or.Item)
                .Include(or => or.Order)
                .Include(or => or.OrderItemChoices)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<OrderRow> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(or => or.Item)
                .Include(or => or.Order)
                .Include(or => or.OrderItemChoices)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}