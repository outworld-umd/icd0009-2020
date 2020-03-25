using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class PersonGroupRepository : EFBaseRepository<PersonGroup, AppDbContext>, IPersonGroupRepository {

        public PersonGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<PersonGroup>> AllAsync() {
            return await RepoDbSet.Include(p => p.Person)
                .Include(p => p.SubjectGroup).ToListAsync();
        }

        public override async Task<PersonGroup> FindAsync(params object[] id) {
            return await RepoDbSet.Include(p => p.Person)
                .Include(p => p.SubjectGroup).FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}