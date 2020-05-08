using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories {

    public class CustomerRepository : EFBaseRepository<Customer, AppDbContext>, ICustomerRepository {
        public CustomerRepository(AppDbContext dbContext) : base(dbContext) { }
    }

}