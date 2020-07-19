using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories {

    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.App.Restaurant, DTO.Restaurant>, IRestaurantRepository {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext, new BaseMapper<Domain.App.Restaurant, DTO.Restaurant>()) { }
    }

}