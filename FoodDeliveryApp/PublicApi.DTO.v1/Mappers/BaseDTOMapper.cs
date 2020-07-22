﻿﻿using AutoMapper;

 namespace PublicApi.DTO.v1.Mappers
 {
     public abstract class BaseDTOMapper<TLeftObject, TRightObject> : DAL.Base.EF.Mappers.BaseMapper<TLeftObject, TRightObject>
         where TRightObject : class?, new()
         where TLeftObject : class?, new()
     {
     }
 }