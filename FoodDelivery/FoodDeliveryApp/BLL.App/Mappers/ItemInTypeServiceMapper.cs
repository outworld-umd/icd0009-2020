﻿using AutoMapper;
using BLL.App.Mappers.Old;
using ee.itcollege.anguzo.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemInTypeServiceMapper: BLLMapper<DALAppDTO.ItemInType, BLLAppDTO.ItemInType>, IItemInTypeServiceMapper
    {
    }
}