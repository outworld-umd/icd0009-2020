using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class PersonGroupRepository : EFBaseRepository<PersonGroup, AppDbContext>, IPersonGroupRepository {

        public PersonGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<IEnumerable<PersonGroup>> AllAsync() {
            return await RepoDbSet.Include(pg => pg.Person)
                .Include(pg => pg.SubjectGroup).ThenInclude(sg => sg.Subject).ToListAsync();
        }
        
        public override IEnumerable<PersonGroup> All() {
            return RepoDbSet.Include(p => p.Person)
                .Include(p => p.SubjectGroup).ThenInclude(sg => sg.Subject).ToList();
        }

        public override async Task<PersonGroup> FindAsync(params object[] id) {
            return await RepoDbSet.Include(pg => pg.Person)
                .Include(pg => pg.SubjectGroup).ThenInclude(sg => sg.Subject)
                .Include(pg => pg.Grades).ThenInclude(g => g.GradeColumn).FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}