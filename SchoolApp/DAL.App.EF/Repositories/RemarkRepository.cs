using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class RemarkRepository : EFBaseRepository<Remark, AppDbContext>, IRemarkRepository  {

        public RemarkRepository(AppDbContext dbContext) : base(dbContext) { }
        
        public override async Task<IEnumerable<Remark>> AllAsync() {
            return await RepoDbSet.Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender).ToListAsync();
        }
        
        public override async Task<Remark> FindAsync(params object[] id) {
            return await RepoDbSet.Include(r => r.Recipient)
                .Include(r => r.RemarkType)
                .Include(r => r.Sender).FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }

}