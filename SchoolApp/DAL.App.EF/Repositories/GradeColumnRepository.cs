using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class GradeColumnRepository : EFBaseRepository<GradeColumn, AppDbContext>, IGradeColumnRepository  {

        public GradeColumnRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<GradeColumn>> AllAsync() {
            return await RepoDbSet.Include(g => g.GradeType)
                .Include(g => g.SubjectGroup).ToListAsync();
        }
        
        public override async Task<GradeColumn> FindAsync(params object[] id) {
            return await RepoDbSet.Include(g => g.GradeType)
                .Include(g => g.SubjectGroup)
                .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}