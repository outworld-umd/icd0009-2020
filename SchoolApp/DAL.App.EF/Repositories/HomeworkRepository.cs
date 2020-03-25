using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class HomeworkRepository : EFBaseRepository<Homework, AppDbContext>, IHomeworkRepository  {

        public HomeworkRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<Homework>> AllAsync() {
            return await RepoDbSet.Include(h => h.SubjectGroup)
                .Include(h => h.Teacher).ToListAsync();
        }
        
        public override async Task<Homework> FindAsync(params object[] id) {
            return await RepoDbSet.Include(h => h.SubjectGroup)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}