using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers.Old
{
    public class IdentityMapper<TLeftObject, TRightObject> : IBaseDALMapper<TLeftObject, TRightObject> 
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
        public TRightObject Map(TLeftObject inObject)
        {
            return inObject as TRightObject ?? default!;
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return inObject as TLeftObject ?? default!;
        }
    }
}