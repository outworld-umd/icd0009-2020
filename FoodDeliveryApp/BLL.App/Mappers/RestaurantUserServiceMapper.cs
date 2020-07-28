using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantUserServiceMapper: BaseBLLMapper<DALAppDTO.RestaurantUser, BLLAppDTO.RestaurantUser>, IRestaurantUserServiceMapper
    {
        public RestaurantUserServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.RestaurantUser, DALAppDTO.RestaurantUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantUser, BLLAppDTO.RestaurantUser>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}