﻿﻿namespace PublicApi.DTO.v1.Mappers.Base
{
    public interface IBaseAPIMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        TRightObject Map(TLeftObject inObject);
        TLeftObject Map(TRightObject inObject);
    }
}