using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class DependenceRepository : EFBaseRepository<Dependence, AppDbContext>, IDependenceRepository  {
 
         public DependenceRepository(AppDbContext dbContext) : base(dbContext) { }
         
         public override async Task<IEnumerable<Dependence>> AllAsync() {
             return await RepoDbSet.Include(d => d.Child)
                 .Include(d => d.DependenceType)
                 .Include(d => d.Parent).ToListAsync();
         }
         
         public override async Task<Dependence> FindAsync(params object[] id) {
             return await RepoDbSet.Include(d => d.Child)
                 .Include(d => d.DependenceType)
                 .Include(d => d.Parent)
                 .FirstOrDefaultAsync(m => m.Id.Equals((Guid) id[0]));
         }
     }
 
 }