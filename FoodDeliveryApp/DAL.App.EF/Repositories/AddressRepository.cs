using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.anguzo.DAL.Base.EF.Repositories;
using ee.itcollege.anguzo.Domain.Identity;

using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class AddressRepository : EFBaseRepository<AppDbContext, AppUser, Domain.App.Address, DTO.Address>, IAddressRepository {
        public AddressRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Address, DTO.Address>())
        {
        }
        public override async Task<IEnumerable<Address>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query
                .Include(a => a.AppUser)
                .ToListAsync();
            var result = domainEntities.Select(e => DALMapper.Map(e));
            return result;
        }

        public override async Task<Address> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var entity = await query
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return DALMapper.Map(entity);
        }


    }

}