using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class PersonFormRepository : EFBaseRepository<PersonForm, AppDbContext>, IPersonFormRepository  {
 
        public PersonFormRepository(AppDbContext dbContext) : base(dbContext) { }
         
        public override async Task<IEnumerable<PersonForm>> AllAsync() {
            return await RepoDbSet.Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person).ToListAsync();
        }
         
        public override async Task<PersonForm> FindAsync(params object[] id) {
            return await RepoDbSet.Include(p => p.Form)
                .Include(p => p.FormRole)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
        }
    }
 
}