using AutoMapper;
using AutoMapper.Configuration;
using ee.itcollege.anguzo.Contracts.BLL.Base.Mappers;

namespace ee.itcollege.anguzo.BLL.Base.Mappers
{
    /// <summary>
    /// Maps using Automapper. No mapper configuration. Property types and names have to match.
    /// </summary>
    /// <typeparam name="TLeftObject"></typeparam>
    /// <typeparam name="TRightObject"></typeparam>
    public abstract class BaseBLLMapper<TLeftObject, TRightObject> : IBaseBLLMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected IMapper Mapper;
        protected readonly MapperConfigurationExpression MapperConfigurationExpression;

        public BaseBLLMapper()
        {
            MapperConfigurationExpression = new MapperConfigurationExpression();
            MapperConfigurationExpression.CreateMap<TLeftObject, TRightObject>();
            MapperConfigurationExpression.CreateMap<TRightObject, TLeftObject>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public virtual TRightObject Map(TLeftObject inObject)
        {
            return Mapper.Map<TLeftObject, TRightObject>(inObject);
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return Mapper.Map<TRightObject, TLeftObject>(inObject);
        }
    }
}