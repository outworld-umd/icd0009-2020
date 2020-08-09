﻿using AutoMapper;
using BLL.App.Mappers.Old;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemOptionServiceMapper: BLLMapper<DALAppDTO.ItemOption, BLLAppDTO.ItemOption>, IItemOptionServiceMapper
    {
    }
}