using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class PersonRepository : EFBaseRepository<Person, AppDbContext>, IPersonRepository {

        public PersonRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<Person>> AllAsync() {
            return await RepoDbSet.Include(p => p.Groups).ToListAsync();
        }

        public override async Task<Person> FindAsync(params object[] id) {
            return await RepoDbSet.Include(p => p.Groups)
                .ThenInclude(pg => pg.SubjectGroup)
                .ThenInclude(sg => sg.Subject)
                .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}