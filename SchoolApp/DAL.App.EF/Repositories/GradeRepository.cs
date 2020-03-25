using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class GradeRepository : EFBaseRepository<Grade, AppDbContext>, IGradeRepository {

        public GradeRepository(AppDbContext dbContext) : base(dbContext) {
        }

        public override async Task<IEnumerable<Grade>> AllAsync() {
            return await RepoDbSet.Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .Include(g => g.GradeColumn).ToListAsync();
        }
        
        public override async Task<Grade> FindAsync(params object[] id) {
            return await RepoDbSet.Include(g => g.AbsenceReason)
                .Include(g => g.Student)
                .Include(g => g.Teacher)
                .Include(g => g.GradeColumn).FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}