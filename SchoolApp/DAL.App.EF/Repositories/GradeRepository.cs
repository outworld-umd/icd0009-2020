using System.Collections.Generic;
using System.Linq;
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
                .Include(g => g.Teacher).ToListAsync();
        }
    }

}