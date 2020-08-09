﻿using ee.itcollege.anguzo.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ILangStringServiceMapper: IBaseBLLMapper<DALAppDTO.LangString, BLLAppDTO.LangString>
    {
        
    }
}