using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Mappers;
using ee.itcollege.anguzo.Domain.Identity;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;

using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class OrderItemChoiceRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.OrderItemChoice, OrderItemChoice>, IOrderItemChoiceRepository {
        public OrderItemChoiceRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Domain.App.OrderItemChoice, OrderItemChoice>()) { }
        
        public override async Task<IEnumerable<OrderItemChoice>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(oic => oic.ItemChoice)
                .Include(oic => oic.OrderRow)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<OrderItemChoice> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(oic => oic.ItemChoice)
                .Include(oic => oic.OrderRow)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }
    }
}