namespace ee.itcollege.anguzo.Contracts.BLL.Base.Mappers
{
    public interface IBaseBLLMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        TRightObject Map(TLeftObject inObject);
        TLeftObject Map(TRightObject inObject);
    }
}