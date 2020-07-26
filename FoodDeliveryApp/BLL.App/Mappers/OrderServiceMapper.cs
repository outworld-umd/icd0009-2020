﻿using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper: BaseBLLMapper<DALAppDTO.Order, BLLAppDTO.Order>, IOrderServiceMapper
    {
        public OrderServiceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Order, DALAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();

            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderRow, DALAppDTO.OrderRow>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderRow, BLLAppDTO.OrderRow>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderItemChoice, DALAppDTO.OrderItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderItemChoice, BLLAppDTO.OrderItemChoice>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.ItemChoice, DALAppDTO.ItemChoice>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ItemChoice, BLLAppDTO.ItemChoice>();
            
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}