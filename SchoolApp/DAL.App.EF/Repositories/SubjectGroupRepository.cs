using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class SubjectGroupRepository : EFBaseRepository<SubjectGroup, AppDbContext>, ISubjectGroupRepository {

        public SubjectGroupRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<SubjectGroup>> AllAsync() {
            return await RepoDbSet.Include(s => s.Subject).ToListAsync();
        }
        
        public override async Task<SubjectGroup> FindAsync(params object[] id) {
            return await RepoDbSet.Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}