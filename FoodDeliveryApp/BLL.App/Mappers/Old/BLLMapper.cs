using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TRightObject, TLeftObject> : BaseBLLMapper<TRightObject, TLeftObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            // add more mappings
            MapperConfigurationExpression.CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Address, DAL.App.DTO.Address>();
            MapperConfigurationExpression.CreateMap<Category, DAL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<Item, DAL.App.DTO.Item>();
            MapperConfigurationExpression.CreateMap<ItemChoice, DAL.App.DTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<ItemInType, DAL.App.DTO.ItemInType>();
            MapperConfigurationExpression.CreateMap<ItemOption, DAL.App.DTO.ItemOption>();
            MapperConfigurationExpression.CreateMap<ItemType, DAL.App.DTO.ItemType>();
            MapperConfigurationExpression.CreateMap<NutritionInfo, DAL.App.DTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<OrderItemChoice, DAL.App.DTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<OrderRow, DAL.App.DTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<Restaurant, DAL.App.DTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<RestaurantCategory, DAL.App.DTO.RestaurantCategory>();
            MapperConfigurationExpression.CreateMap<RestaurantUser, DAL.App.DTO.RestaurantUser>();
            MapperConfigurationExpression.CreateMap<WorkingHours, DAL.App.DTO.WorkingHours>();
            MapperConfigurationExpression.CreateMap<LangString, DAL.App.DTO.LangString>();
            MapperConfigurationExpression.CreateMap<Translation, DAL.App.DTO.Translation>();

            
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
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.LangString, LangString>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Translation, Translation>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}