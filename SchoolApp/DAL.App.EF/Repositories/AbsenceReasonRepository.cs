using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class AbsenceReasonRepository : EFBaseRepository<AbsenceReason, AppDbContext>, IAbsenceReasonRepository {

        public AbsenceReasonRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<AbsenceReason>> AllAsync() {
            return await RepoDbSet.Include(a => a.Creator)
                .Include(a => a.Student).ToListAsync();
        }
        
        public override async Task<AbsenceReason> FindAsync(params object[] id) {
            return await RepoDbSet.Include(a => a.Creator)
                .Include(a => a.Student).FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}