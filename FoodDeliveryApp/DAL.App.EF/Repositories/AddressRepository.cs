using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories {

    public class AddressRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Address, DTO.Address>, IAddressRepository {
        public AddressRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Address, DTO.Address>())
        {
        }



    }

}