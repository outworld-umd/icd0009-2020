﻿using AutoMapper;
using AutoMapper.Configuration;
 using ee.itcollege.anguzo.DTO.Identity;


namespace PublicApi.DTO.v1.Mappers.Base
{
    public class APIMapper<TLeftObject, TRightObject> : BaseAPIMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public APIMapper() : base()
        {
            // add more mappings

            MapperConfigurationExpression.CreateMap<Identity.AppUser, AppUser>();
            MapperConfigurationExpression.CreateMap<Address, BLL.App.DTO.Address>();
            MapperConfigurationExpression.CreateMap<Category, BLL.App.DTO.Category>();
            MapperConfigurationExpression.CreateMap<Item, BLL.App.DTO.Item>()
                .ForMember(d => d.ItemOptions, 
                    opt => opt.MapFrom(src => src.Options));
            MapperConfigurationExpression.CreateMap<ItemChoice, BLL.App.DTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<ItemOption, BLL.App.DTO.ItemOption>()
                .ForMember(d => d.ItemChoices, 
                    opt => opt.MapFrom(src => src.Choices));
            MapperConfigurationExpression.CreateMap<ItemType, BLL.App.DTO.ItemType>();
            MapperConfigurationExpression.CreateMap<NutritionInfo, BLL.App.DTO.NutritionInfo>();
            MapperConfigurationExpression.CreateMap<OrderMapper, BLL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<OrderItemChoice, BLL.App.DTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<OrderRow, BLL.App.DTO.OrderRow>()
                .ForMember(d => d.OrderItemChoices, 
                    opt => opt.MapFrom(src => src.Choices));
            MapperConfigurationExpression.CreateMap<Restaurant, BLL.App.DTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<WorkingHours, BLL.App.DTO.WorkingHours>();


            MapperConfigurationExpression.CreateMap<AppUser, Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Address, Address>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, Category>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryListView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, Item>()
                .ForMember(d => d.Options, 
                    opt => opt.MapFrom(src => src.ItemOptions));
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, ItemView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemChoice, ItemChoice>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemOption, ItemOption>()
                .ForMember(d => d.Choices, 
                    opt => opt.MapFrom(src => src.ItemChoices));
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ItemType, ItemType>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.NutritionInfo, NutritionInfo>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Order, OrderMapper>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderRow, OrderRow>()
                .ForMember(d => d.Choices, 
                    opt => opt.MapFrom(src => src.OrderItemChoices));
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderItemChoice, OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, Restaurant>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, RestaurantView>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.WorkingHours, WorkingHours>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}