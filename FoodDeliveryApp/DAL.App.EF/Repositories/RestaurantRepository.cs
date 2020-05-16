using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories {

    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.Restaurant, DTO.Restaurant>, IRestaurantRepository {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Restaurant, DTO.Restaurant>()) { }
    }

}