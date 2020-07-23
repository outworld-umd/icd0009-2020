using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using DAL.Base.EF.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TRightObject, TLeftObject> : BaseMapper<TRightObject, TLeftObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            // add more mappings
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, AppUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Address, Address>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Category, Category>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Item, Item>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemChoice, ItemChoice>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemInType, ItemInType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemOption, ItemOption>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ItemType, ItemType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.NutritionInfo, NutritionInfo>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Order, Order>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.OrderItemChoice, OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.OrderRow, OrderRow>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Restaurant, Restaurant>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.RestaurantCategory, RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.RestaurantUser, RestaurantUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.WorkingHours, WorkingHours>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}