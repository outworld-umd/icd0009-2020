using AutoMapper;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Domain.App.Address, DAL.App.DTO.Address>();
            MapperConfigurationExpression.CreateMap<Domain.App.Category, DAL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<Domain.App.Item, DAL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<Domain.App.ItemChoice, DAL.App.DTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<Domain.App.ItemInType, DAL.App.DTO.ItemInType>();
            MapperConfigurationExpression.CreateMap<Domain.App.ItemOption, DAL.App.DTO.ItemOption>();
            MapperConfigurationExpression.CreateMap<Domain.App.ItemType, DAL.App.DTO.ItemType>();
            MapperConfigurationExpression.CreateMap<Domain.App.NutritionInfo, DAL.App.DTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<Domain.App.Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<Domain.App.OrderItemChoice, DAL.App.DTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<Domain.App.OrderRow, DAL.App.DTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<Domain.App.Restaurant, DAL.App.DTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Domain.App.RestaurantCategory, DAL.App.DTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<Domain.App.RestaurantUser, DAL.App.DTO.RestaurantUser>();
            MapperConfigurationExpression.CreateMap<Domain.App.WorkingHours, DAL.App.DTO.WorkingHours>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}