using System;
using ee.itcollege.anguzo.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : IBaseDALMapper<TLeftObject, TRightObject> 
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
        public TRightObject Map(TLeftObject inObject)
        {
            return (inObject as TRightObject ?? default!) ?? throw new InvalidOperationException();
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return (inObject as TLeftObject ?? default!) ?? throw new InvalidOperationException();
        }
    }
}