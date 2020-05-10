using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class AddressRepository : EFBaseRepository<Address, AppDbContext>, IAddressRepository {
        public AddressRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Address>> AllAsync() {
            return await RepoDbSet.ToListAsync();
        }

        public override async Task<Address> FindAsync(params object[] id) {
            return await RepoDbSet
                .FirstOrDefaultAsync(m => m.Id == (Guid) id[0]);
        }

    }

}